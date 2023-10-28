using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damir_Filipovic_HCI2023
{
    public partial class Shop : Form
    {
        public Shop()
        {
            InitializeComponent();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            new StartPage().Show();
        }

        private void trackBar1_DragDrop(object sender, DragEventArgs e)
        {
            String priceVal = trackBar1.Value.ToString();
            priceLabel.Text = priceVal;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            String priceVal = trackBar1.Value.ToString();
            priceLabel.Text = priceVal;
        }
    }
}
