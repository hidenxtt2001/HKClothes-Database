using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HkClothes.model
{
    public class Product
    {
        public string pid { get; set; }
        public string product_name { get; set; }
        public string type { get; set; }
        public double price { get; set; }
        public string image_url { get; set; }
    }
}
