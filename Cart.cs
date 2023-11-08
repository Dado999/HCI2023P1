using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Damir_Filipovic_HCI2023
{
    public partial class Cart : Form
    {
        private int totalPrice;
        public Cart()
        {
            InitializeComponent();
            Program.UpdateTheme(this);
            ChangeLanguage();
            foreach (Product product in Shop.clickedProducts)
                CreateCartItem(product);
            calculatePriceOfCart();
        }
        //Loading in labels from the cart
        private void CreateCartItem(Product p)
        {
            Panel panel = CreatePanel();
            panel.Controls.Add(CreatePanelPicture(p.picture.BackgroundImage));
            panel.Controls.Add(CreatePanelButton());
            if (Program.currentUser.language == "English")
            {
                panel.Controls.Add(CreatePanelLabel("Price: " + (p.price * p.clickLimit).ToString() + "KM", 103, 5));
                panel.Controls.Add(CreatePanelLabel("Quantity: " + p.clickLimit.ToString(), 103, 40));
            }
            else
            {
                panel.Controls.Add(CreatePanelLabel((Program.currentUser.language == "Serbian" ? "Cijena: " : "Precio ") + (p.price * p.clickLimit).ToString() + "KM", 103, 5));
                panel.Controls.Add(CreatePanelLabel((Program.currentUser.language == "Serbian" ? "Kolicina: " : "Cantidad ") + p.clickLimit.ToString(), 103, 40));
            }
            Label nameOfProduct = CreatePanelLabel(p.name, 0, 0);
            nameOfProduct.SendToBack();
            nameOfProduct.ForeColor = Color.White;
            panel.Controls.Add(nameOfProduct);
            flowLayoutPanel1.Controls.Add(panel);
        }
        private Panel CreatePanel()
        {
            Panel panel = new Panel();
            panel.Height = 80;
            panel.Width = 390;
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.Fixed3D;
            return panel;
        }
        private PictureBox CreatePanelPicture(Image img)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackgroundImage = img;
            pictureBox.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox.Dock = DockStyle.Left;
            pictureBox.BorderStyle = BorderStyle.Fixed3D;
            return pictureBox;
        }
        private Button CreatePanelButton()
        {
            Button btn = new Button();
            btn.Width = 95;
            btn.BackColor = Color.Firebrick;
            btn.BackgroundImage = Properties.Resources.trashBin;
            btn.Dock = DockStyle.Right;
            btn.BackgroundImageLayout = ImageLayout.Zoom;
            btn.Click += RemoveItemButtonClick;
            return btn;
        }
        private Label CreatePanelLabel(string text, int X, int Y)
        {
            Label price = new Label();
            price.Text = text;
            price.AutoSize = true;
            price.Location = new Point(X, Y);
            price.Font = new Font(price.Font.FontFamily, 16, FontStyle.Bold);
            return price;
        }

        private void RemoveItemButtonClick(object sender, EventArgs e)
        {
            Panel parent = (Panel)((Button)sender).Parent;
            Product p = GetItemFromPanel(parent);
            Label quantity = GetLabelFromPanel(parent, "Quantity");
            Label price = GetLabelFromPanel(parent, "Price");

            if (--p.clickLimit == 0)
            {
                flowLayoutPanel1.Controls.Remove(parent);
                Shop.clickedProducts.Remove(p);
                calculatePriceOfCart();
            }
            else
            {
                quantity.Text = "Quantity: " + p.clickLimit.ToString();
                price.Text = "Price:" + (p.clickLimit * p.price).ToString() + "KM";
                calculatePriceOfCart();
            }
        }
        private Product GetItemFromPanel(Panel panel)
        {
            Product productToBeRemoved = null;
            foreach (Product p in Shop.clickedProducts)
            {
                foreach (Control c in panel.Controls)
                {
                    if (c is Label && c.Text.Equals(p.name))
                    {
                        productToBeRemoved = p; break;
                    }
                }
            }
            return productToBeRemoved;
        }
        private Label GetLabelFromPanel(Panel panel, string nameOfLabel)
        {
            foreach (Control c in panel.Controls)
            {
                if (c is Label && c.Text.Contains(nameOfLabel))
                    return c as Label;
            }
            return null;
        }

        private void calculatePriceOfCart()
        {
            totalPrice = 0;
            if (Shop.clickedProducts.Count == 0)
            {
                totalPrice = 0;
                priceLabel.Text = "Total price: " + totalPrice.ToString() + "KM";
            }
            else
            {
                foreach (Product p in Shop.clickedProducts)
                    totalPrice += (p.price * p.clickLimit);
                priceLabel.Text = "Total price: " + totalPrice.ToString() + "KM";
            }
        }

        private void orderBtn_Click(object sender, EventArgs e)
        {
            if(totalPrice==0)
                MessageBox.Show("Cart empty!","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                new Order(totalPrice).Show();
        }
        private void ChangeLanguage()
        {
            if (Program.currentUser.language == "English")
                Program.UpdateLanguage(splitContainer1, new ResourceManager("Damir_Filipovic_HCI2023.Properties.LanguageEN", typeof(StartPage).Assembly));
            else if (Program.currentUser.language == "Serbian")
                Program.UpdateLanguage(splitContainer1, new ResourceManager("Damir_Filipovic_HCI2023.Properties.LanguageSRB", typeof(StartPage).Assembly));
            else
                Program.UpdateLanguage(splitContainer1, new ResourceManager("Damir_Filipovic_HCI2023.Properties.LanguageESP", typeof(StartPage).Assembly));
        }
    }
}
