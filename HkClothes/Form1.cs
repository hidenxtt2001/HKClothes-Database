﻿using Google.Cloud.Firestore;
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

namespace HkClothes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FirestoreDb database;
        string imageProduct;
        static List<string> sizeCheck;
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"hkclothes-d2a8d-firebase-adminsdk-8zdze-71fc1a3cb9.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("hkclothes-d2a8d");
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  


                var stream = new MemoryStream(File.ReadAllBytes(open.FileName.ToString()));
                var img = new Bitmap(stream);

                imageProduct = open.FileName;
                pictureBox1.Image = img;


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
            foreach( var i in product_size.CheckedItems) { }
            using (LoadingForm loadingForm = new LoadingForm( uploadData))
            {
                loadingForm.ShowDialog();
                clearData();
                MessageBox.Show("Thêm data thành công");
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
            price.Clear();
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
            int type = 0;
            switch (this.Invoke(new Func<string>(()=> category.SelectedItem.ToString())).ToString())
            {
                case "Shirt":
                    type = 1;
                    break;
                case "T-Shirt":
                    type = 2;
                    break;
                case "Hoodies":
                    type = 3;
                    break;
                case "Short":
                    type = 4;
                    break;
                case "Pants":
                    type = 5;
                    break;
                case "Sweatshirt":
                    type = 6;
                    break;
            }
            var product = new Product() { pid = id, product_name = name_product.Text, image_url = imageUrl, price = double.Parse(price.Text), type = type };
            var json = JsonConvert.SerializeObject(product);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            await database.Collection("shopstore").Document("products").Collection("product_detail").Document(id).SetAsync(dictionary);
            var sizeItem = (CheckedListBox.CheckedItemCollection)this.Invoke(new Func<CheckedListBox.CheckedItemCollection>(() => product_size.CheckedItems));
            foreach(var i in sizeItem)
            {
                int sid = 0;
                switch (i.ToString())
                {
                    case "S":
                        sid = 1;
                        break;
                    case "M":
                        sid = 2;
                        break;
                    case "L":
                        sid = 3;
                        break;
                    case "XL":
                        sid = 4;
                        break;
                    case "XXL":
                        sid = 5;
                        break;
                }
                string psid = Guid.NewGuid().ToString("N");
                json = JsonConvert.SerializeObject(new ProductSize() { psid = psid, pid = id, sid = sid });
                dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                await database.Collection("shopstore").Document("products").Collection("product_size").Document(psid).SetAsync(dictionary);
            }
        }

        private bool checkValid()
        {
            if (name_product.Text.Trim() == string.Empty) return false;
            if (category.SelectedIndex == -1) return false;
            if (product_size.CheckedItems.Count == 0) return false;
            if (price.Text.Trim() == string.Empty) return false;
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
    }
}