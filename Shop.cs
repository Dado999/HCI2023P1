using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Damir_Filipovic_HCI2023
{
    public partial class Shop : Form
    {
        bool componentsCollapsed=true;
        bool peripheralsCollapsed=true;
        bool storageCollapsed = true;
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HCI"].ConnectionString;
        private Dictionary<PictureBox,Label> pictureBoxLabelMap = new Dictionary<PictureBox,Label>();
        private List<Product> presentedProducts = new List<Product>();
        private List<Product> clickedProducts = new List<Product>();

        public Shop()
        {
            InitializeComponent();
            Program.UpdateTheme(this);
            populateFlowLayoutPanel(@"SELECT * FROM product");
        }

        //Collapsing animations
        private void componentsButton_Click(object sender, EventArgs e) { componentsTimer.Start(); if (componentsCollapsed) { emptyFlowLayoutPanel(); populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(1,2,3,4,5)"); } }
        private void peripheralsBtn_Click(object sender, EventArgs e) { peripheralsTimer.Start(); if (peripheralsCollapsed) { emptyFlowLayoutPanel(); populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(6,7,8,9)"); } }
        private void storageBtn_Click(object sender, EventArgs e) { storageTimer.Start(); if (storageCollapsed) { emptyFlowLayoutPanel(); populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(10,11)"); } }
        private void componentsTimer_Tick(object sender, EventArgs e) { CollapsingAnimation(ref componentsCollapsed, componentsTimer, componentsPanel); }
        private void peripheralsTimer_Tick(object sender, EventArgs e) { CollapsingAnimation(ref peripheralsCollapsed, peripheralsTimer, peripheralsPanel); }
        private void storageTimer_Tick(object sender, EventArgs e) { CollapsingAnimation(ref storageCollapsed, storageTimer, storagePanel); }
        private void CollapsingAnimation(ref bool collapse, Timer timer, Panel panel)
        {
            if (collapse)
            {
                panel.Height += 10;
                if (panel.Height == panel.MaximumSize.Height)
                {
                    collapse = false;
                    timer.Stop();
                }
            }
            else
            {
                panel.Height -= 10;
                if (panel.Height == panel.MinimumSize.Height)
                {
                    collapse = true;
                    timer.Stop();
                }
            }
        }

        //Settings, Cart and Log out buttons click
        private void settingsBtn_Click_1(object sender, EventArgs e)
        {
            Settings set = new Settings();
            set.Show();
            this.Close();
        }
        private void cartBtn_Click(object sender, EventArgs e)
        {

        }
        private void logoutBtn_Click_1(object sender, EventArgs e)
        {
            this.Close();
            new StartPage().Show();
        }

        //Submenu button click - Components
        private void componentsGCBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel(); 
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(1)");
        }
        private void componentsCpuBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel();
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(2)");
        }
        private void componentsRAMBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel();
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(3)");
        }
        private void componentMotherboardBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel();
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(4)");
        }
        private void componentPSBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel();
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(5)");
        }

        //Submenu button click - Peripherals
        private void peripheralsMouseBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel(); 
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(6)");
        }
        private void peripheralsKeyboardBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel();
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(7)");
        }
        private void peripheralsSpeakersBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel();
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(8)");
        }
        private void peripheralsHeadphonesBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel();
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(9)");
        }

        //Submenu button click - Storage
        private void storageSSDBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel();
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(10)");
        }
        private void storageHDDBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel(); 
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(11)");
        }

        //Submenu button click - Gaming and Systems
        private void gamingBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel();
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(12)");
        }
        private void systemsBtn_Click(object sender, EventArgs e)
        {
            emptyFlowLayoutPanel();
            populateFlowLayoutPanel(@"SELECT * FROM product WHERE categoryID IN(13)");
        }

        //Populating the FlowPanelLayout with selected items
        private void populateFlowLayoutPanel(string mysqlCommand)
        {
            if (layoutPanel.Controls.Count > 0)
                emptyFlowLayoutPanel();
            else
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand mySqlCommand = conn.CreateCommand();
                mySqlCommand.CommandText = mysqlCommand;
                using (MySqlDataReader dataReader = mySqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        PictureBox pic = createPictureBox(dataReader.GetString("description"));

                        Label nameLabel = CreateNameLabel(dataReader.GetString("name"));
                        pic.Controls.Add(nameLabel);


                        Label priceLabel = CreatePriceLabel(dataReader.GetString("price"));
                        pic.Controls.Add(priceLabel);
                        pictureBoxLabelMap[pic] = priceLabel;

                        layoutPanel.Controls.Add(pic);
                        presentedProducts.Add(new Product(dataReader.GetString("name"), int.Parse(dataReader.GetString("price")), int.Parse(dataReader.GetString("quantity")), pic));
                    }
                }
            }
        }
        private PictureBox createPictureBox(string photoPath)
        {
            PictureBox pic = new PictureBox();
            pic.Height = 200;
            pic.Width = 200;
            pic.BackgroundImage = Image.FromFile(photoPath);
            pic.BackColor = Color.White;
            pic.BackgroundImageLayout = ImageLayout.Center;
            pic.Margin = new Padding(5, 5, 5, 30);
            pic.BorderStyle = BorderStyle.Fixed3D;
            pic.Cursor = Cursors.Hand;
            pic.Click += PictureBox_Click;
            return pic;
        }
        private Label CreateNameLabel(string nameOfLabel)
        {
            Label name = new Label();
            name.Text = nameOfLabel;
            name.Dock = DockStyle.Top;
            name.ForeColor = Color.White;
            name.BackColor = Color.FromArgb(30, 97, 170);
            name.Font = new Font(name.Font, FontStyle.Bold);
            name.Font = new Font(name.Font.FontFamily, 13);
            return name;
        }
        private Label CreatePriceLabel(string priceOfLabel)
        {
            Label price = new Label();
            price.Text = priceOfLabel + "KM";
            price.Dock = DockStyle.Bottom;
            price.ForeColor = Color.Black;
            price.BackColor = Color.Orange;
            price.Font = new Font(price.Font, FontStyle.Bold);
            price.Font = new Font(price.Font.FontFamily, 12);
            return price;
        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPicture = (PictureBox)sender;
            Product product = presentedProducts.FirstOrDefault(item => item.picture == clickedPicture);
            if(!synchronizeClickedProductWithClickedProductsList(product)) { clickedProducts.Add(product); }
            if (product.clickLimit == product.quantity)
            {
                MessageBox.Show("No more items available!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else 
            {
                product.clickLimit += 1;
                if (pictureBoxLabelMap.ContainsKey(clickedPicture))
                {
                    Label priceOfItem = pictureBoxLabelMap[clickedPicture];
                    int price = GetPrice(priceOfItem.Text);
                    cartValueCurrentLabel.Text = (price + GetPrice(cartValueCurrentLabel.Text)).ToString();
                }
            }
        }
        private int GetPrice(string priceOfItem)
        {
            string numericText = new string(priceOfItem.Where(char.IsDigit).ToArray());
            if (int.TryParse(numericText, out int price))
                return price;
            else
                return 0;
        }
        private void emptyFlowLayoutPanel()
        {
            while (layoutPanel.Controls.Count > 0) 
                layoutPanel.Controls.Remove(layoutPanel.Controls[0]);
            presentedProducts.Clear();
        }
        
        public bool synchronizeClickedProductWithClickedProductsList(Product clickedProduct)
        {
            for (int i = 0; i < clickedProducts.Count; i++)
                if (clickedProducts[i].name.Equals(clickedProduct.name))
                {
                    clickedProduct.clickLimit = clickedProducts[i].clickLimit;
                    clickedProducts[i] = clickedProduct;
                    return true;
                }
            return false;

        }
    }
    }
