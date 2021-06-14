using HkClothes.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HkClothes
{
    public partial class UpdateSale : Form
    {
        private Product product;
        public UpdateSale(Product product)
        {
            InitializeComponent();
            this.product = product;
        }

        private void set_sale_Click(object sender, EventArgs e)
        {
            if (double.Parse(percent.Text) > 100 || double.Parse(percent.Text) < 0)
            {
                MessageBox.Show("% sale không hợp lý");
            }
            else
            {
                using (LoadingForm form = new LoadingForm(setSale))
                {
                    form.ShowDialog();
                    MessageBox.Show("Set sale thanh cong");
                    this.Close();
                }
            }
        }


        private void setSale()
        {
            setSaleTask().Wait();
        }

        private async Task setSaleTask()
        {
            Form1.database.Collection("shopstore").Document("products").Collection("product_sale").WhereEqualTo("pid", product.pid).Listen(async (snapshot) =>
            {
                if (snapshot.Documents.Count > 0)
                {
                    var json = JsonConvert.SerializeObject(snapshot.Documents[0].ToDictionary(), Newtonsoft.Json.Formatting.Indented);
                    var j = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    var n = JsonConvert.DeserializeObject<ProductSale>(json);
                    await Form1.database.Collection("shopstore").Document("products").Collection("product_sale").Document(n.sale_id).SetAsync(j);
                }
                else
                {
                    string sale_id = Guid.NewGuid().ToString("N");
                    ProductSale productSale = new ProductSale()
                    {
                        sale_id = sale_id,
                        pid = product.pid,
                        percent = (double)this.Invoke(new Func<double>(() => double.Parse(percent.Text)))
                    };
                    var json = JsonConvert.SerializeObject(productSale, Newtonsoft.Json.Formatting.Indented);
                    var j = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    await Form1.database.Collection("shopstore").Document("products").Collection("product_sale").Document(sale_id).SetAsync(j);
                }
            });
        }
        private void percent_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
