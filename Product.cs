using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damir_Filipovic_HCI2023
{
    public class Product
    {
        public int price { get; set; }
        public int quantity { get; set; }
        public string name { get; set; }
        public PictureBox picture { get; set; }
        public int clickLimit = 0;

        public Product(string name,int price, int quantity,PictureBox picture)
        {
            this.name = name;
            this.price = price;
            this.quantity = quantity;
            this.picture = picture;
        }
    }
}
