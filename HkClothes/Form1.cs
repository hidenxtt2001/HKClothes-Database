using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Storage;
using Firebase.Auth;
using HkClothes.model;
using Newtonsoft.Json;
using System.Reflection;

namespace HkClothes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, data_products, new object[] { true });
        }

        FirestoreDb database;
        string imageProduct;
        static List<string> sizeCheck;
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"hkclothes-d2a8d-firebase-adminsdk-8zdze-71fc1a3cb9.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("hkclothes-d2a8d");
            loadProducts();
        }

        #region Add Product
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  

                try
                {
                    var stream = new MemoryStream(File.ReadAllBytes(open.FileName.ToString()));
                    var img = new Bitmap(stream);

                    imageProduct = open.FileName;
                    pictureBox1.Image = img;
                }
                catch (Exception)
                {
                    MessageBox.Show("Hình ảnh lỗi Format");
                }


                // image file path  
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void add_product_Click(object sender, EventArgs e)
        {
            if (!checkValid()) { MessageBox.Show("Nhập đầy đủ thông tin"); return; }
            using (LoadingForm loadingForm = new LoadingForm(uploadData))
            {
                loadingForm.ShowDialog();
                MessageBox.Show("Thêm data thành công");
                clearData();

            }
        }

        private void clearData()
        {
            name_product.Clear();
            category.SelectedIndex = -1;
            foreach (int i in product_size.CheckedIndices)
            {
                product_size.SetItemCheckState(i, CheckState.Unchecked);
            }
            product_price.Clear();
            imageProduct = null;
            pictureBox1.Image = null;
        }

        public void uploadData()
        {
            uploadDataAsync().Wait();


        }

        private async Task uploadDataAsync()
        {

            string id = Guid.NewGuid().ToString("N");
            string imageUrl = await upLoadimageAsync(id);
            string type = null;
            switch (this.Invoke(new Func<string>(() => category.SelectedItem.ToString())).ToString())
            {
                case "Shirt":
                    type = 1.ToString();
                    break;
                case "T-Shirt":
                    type = 2.ToString();
                    break;
                case "Hoodies":
                    type = 3.ToString();
                    break;
                case "Short":
                    type = 4.ToString();
                    break;
                case "Pants":
                    type = 5.ToString();
                    break;
                case "Sweatshirt":
                    type = 6.ToString();
                    break;
            }
            var product = new Product() { pid = id, product_name = name_product.Text, image_url = imageUrl, price = double.Parse(product_price.Text), type = type };
            var json = JsonConvert.SerializeObject(product);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            await database.Collection("shopstore").Document("products").Collection("product_detail").Document(id).SetAsync(dictionary);
            var sizeItem = (CheckedListBox.CheckedItemCollection)this.Invoke(new Func<CheckedListBox.CheckedItemCollection>(() => product_size.CheckedItems));
            foreach (var i in sizeItem)
            {
                string sid = null;
                switch (i.ToString())
                {
                    case "S":
                        sid = 1.ToString();
                        break;
                    case "M":
                        sid = 2.ToString();
                        break;
                    case "L":
                        sid = 3.ToString();
                        break;
                    case "XL":
                        sid = 4.ToString();
                        break;
                    case "XXL":
                        sid = 5.ToString();
                        break;
                }
                string psid = Guid.NewGuid().ToString("N");
                json = JsonConvert.SerializeObject(new ProductSize() { psid = psid, pid = id, sid = sid });
                dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                await database.Collection("shopstore").Document("products").Collection("product_size").Document(psid).SetAsync(dictionary);
            }
        }

        private bool checkValid()
        {
            if (name_product.Text.Trim() == string.Empty) return false;
            if (category.SelectedIndex == -1) return false;
            if (product_size.CheckedItems.Count == 0) return false;
            if (product_price.Text.Trim() == string.Empty) return false;
            if (imageProduct == null) return false;
            return true;
        }

        private async Task<string> upLoadimageAsync(string pid)
        {
            /*var auth = new FirebaseAuthProvider(new FirebaseConfig("71fc1a3cb97e5305f138880f934e94d21e83df6e"));
            var a = await auth.SignInWithEmailAndPasswordAsync("hung@gmail.com", "hung");
            var options = new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true,
            };*/
            var task = new FirebaseStorage("hkclothes-d2a8d.appspot.com").Child("shopstore").Child("product").Child(pid + ".jpg").PutAsync(File.Open(imageProduct, FileMode.Open));
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
            var downloadUrl = await task;
            return downloadUrl;
        }
        #endregion


        #region Load Data
        private void loadProducts()
        {
            var k = database.Collection("shopstore").Document("products").Collection("product_detail");
            FirestoreChangeListener listener = k.Listen((snapshot) =>
            {
                this.Invoke(new Action(() => { data_products.Rows.Clear(); }));
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        var json = JsonConvert.SerializeObject(documentSnapshot.ToDictionary(), Newtonsoft.Json.Formatting.Indented);

                        var n = JsonConvert.DeserializeObject<Product>(json);
                        var image = imageUrlProduct(n.image_url);
                        this.Invoke(new Action(() =>
                        {
                            int i = data_products.Rows.Add();
                            data_products.Rows[i].Cells["stt"].Value = i + 1;
                            data_products.Rows[i].Cells["image"].Value = image;
                            data_products.Rows[i].Cells["name"].Value = n.product_name;
                            string type = null;
                            switch (n.type)
                            {
                                case "1":
                                    type = "Shirt";
                                    break;
                                case "2":
                                    type = "T-Shirt";
                                    break;
                                case "3":
                                    type = "Hoodies";
                                    break;
                                case "4":
                                    type = "Short";
                                    break;
                                case "5":
                                    type = "Pants";
                                    break;
                                case "6":
                                    type = "Sweatshirt";
                                    break;
                            }
                            data_products.Rows[i].Cells["type"].Value = type;
                            data_products.Rows[i].Cells["price"].Value = n.price.ToString();
                            data_products.Rows[i].Tag = n;
                        }));

                    }
                }

            });
        }

        private Bitmap imageUrlProduct(string url)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(url);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            Bitmap bitmap2 = new Bitmap(responseStream);
            return bitmap2;
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Chắc ko ??", "Alert", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (LoadingForm loadingForm = new LoadingForm(deleteProduct))
                {
                    loadingForm.ShowDialog();
                }
            }
        }

        private void deleteProduct()
        {
            var k = (Product)this.Invoke(new Func<Product>(() => (Product)data_products.SelectedRows[0].Tag));
            deleteProductAsync(k).Wait();
        }

        private async Task deleteProductAsync(Product k)
        {
            await database.Collection("shopstore").Document("products").Collection("product_detail").Document(k.pid).DeleteAsync();
            var task = new FirebaseStorage("hkclothes-d2a8d.appspot.com").Child("shopstore").Child("product").Child(k.pid + ".jpg").DeleteAsync();
            task.Wait();
            database.Collection("shopstore").Document("products").Collection("product_size").WhereEqualTo("pid", k.pid).Listen(async (snapshot) =>
            {
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        await database.Collection("shopstore").Document("products").Collection("product_size").Document(documentSnapshot.GetValue<string>("psid")).DeleteAsync();
                    }
                }

            });

            database.Collection("shopstore").Document("products").Collection("product_sale").WhereEqualTo("pid", k.pid).Listen(async (snapshot) =>
            {
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        await database.Collection("shopstore").Document("products").Collection("product_size").Document(documentSnapshot.GetValue<string>("psid")).DeleteAsync();
                    }
                }

            });

        }

        private void data_products_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            data_products.ClearSelection();
            if (e.RowIndex >= 0)
                data_products.Rows[e.RowIndex].Selected = true;
        }
        #endregion


    }
}
