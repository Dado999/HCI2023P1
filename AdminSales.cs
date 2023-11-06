using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Damir_Filipovic_HCI2023
{
    public partial class AdminSales : Form
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HCI"].ConnectionString;
        List<databaseOrder> databaseOrders = new List<databaseOrder>();
        List<Product> storageItems = new List<Product>();
        DataGridView storage = new DataGridView();
        DataGridView orders = new DataGridView();
        public AdminSales()
        {
            InitializeComponent();
        }
        private void ordersBtn_Click(object sender, EventArgs e)
        {
            storage = null;
            if (databaseOrders.Count == 0)
            {
                orders = CreateDataGridView();
                splitContainer1.Panel2.Controls.Add(orders);
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from hci.order";

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                        databaseOrders.Add(new databaseOrder(
                            reader.GetInt32("idOrder"),
                            reader.GetString("name"),
                            reader.GetString("surname"),
                            reader.GetString("number"),
                            reader.GetString("city"),
                            reader.GetInt32("totalPrice"),
                            reader.GetString("orderDate"),
                            reader.GetInt32("userID"),
                            reader.GetInt32("adressID")));
                    orders.DataSource = databaseOrders;
                }
            }
            else
            {
                orders = CreateDataGridView();
                splitContainer1.Panel2.Controls.Add(orders);
                orders.DataSource = databaseOrders;
            }
        }
        private void storage_Click(object sender, EventArgs e)
        {
            orders = null;
            if (storageItems.Count == 0)
            {
                storage = CreateDataGridView();
                splitContainer1.Panel2.Controls.Add(storage);
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from product";

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            storageItems.Add(new Product(
                                reader.GetString("name"),
                                reader.GetInt32("price"),
                                reader.GetInt32("quantity"),
                                null,
                                reader.GetString("description")));
                    }
                    storage.DataSource = storageItems;
                }
            }
            else
            {
                storage = CreateDataGridView();
                splitContainer1.Panel2.Controls.Add(storage);
                storage.DataSource = storageItems;
            }
        }
        private DataGridView CreateDataGridView()
        {
            foreach (Control c in splitContainer1.Panel2.Controls)
                c.Hide();
            DataGridView dg = new DataGridView();
            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dg.Dock = DockStyle.Fill;
            return dg;
        }
        private void orderBtn_Click(object sender, EventArgs e)
        {
            foreach (Control c in splitContainer1.Panel2.Controls)
                c.Hide();
            orderListPanel.Visible = true;
            addItemPanel.Visible = true;
            foreach(Product p in storageItems)
            {
                itemsListComboBox.Items.Add(p.name);
            }
            populateCategoryComboBox();
        }

        private void addItemToInvoiceBtn_Click(object sender, EventArgs e)
        {
            int quantity = quantityTextBox.Text.Length>0 ? int.Parse(quantityTextBox.Text) : 1;
            string item = itemsListComboBox.Text;

            Product p = storageItems.Find(prod => prod.name == item);
            ListViewItem itemToBeAdded = new ListViewItem(p.name);
            itemToBeAdded.SubItems.Add(((p.price*0.7)*quantity).ToString());
            itemToBeAdded.SubItems.Add(quantity.ToString());
            invoiceItems.Items.Add(itemToBeAdded);

        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            int categoryId;
            if (categoryTB.Text.Length == 0)
            {
                categoryId = findCategoryID(categoryComboBox.Text);
                if (categoryId == -1)
                    MessageBox.Show("No category found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    addItem(categoryId);
            }
            else
                addItem(addNewCategory(categoryTB.Text));
           
        }
        private int findCategoryID(string name)
        {

            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();

            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = @"select * from category where categoryName=@name";
            command.Parameters.AddWithValue("@name", name);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                    return reader.GetInt32("idCategory");
                else
                    return -1;

            }
        }
        private void addItem(int categoryId)
        {
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = @"insert into product(name, price, quantity, description, orderedItemsID, invoiceItemsID, categoryID) 
                                    values(@productname,@productprice,@productquantity,@productdescription,null,null,@productcategory)";
            command.Parameters.AddWithValue("@productname", nameTB.Text);
            command.Parameters.AddWithValue("@productprice", int.Parse(priceTB.Text));
            command.Parameters.AddWithValue("@productquantity", int.Parse(quantityTB.Text));
            command.Parameters.AddWithValue("@productdescription", pictureTB.Text);
            command.Parameters.AddWithValue("@productcategory", categoryId);
            command.ExecuteNonQuery();
            MessageBox.Show("Sucessfully added the new item!","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private int addNewCategory(string categoryName)
        {
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();

            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = @"insert into category(categoryName) values(@name); select last_insert_id()";
            command.Parameters.AddWithValue("@name", categoryName);
            return Convert.ToInt32(command.ExecuteScalar());

        }
        private void populateCategoryComboBox()
        {
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();

            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = "select * from category";

            using(MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    categoryComboBox.Items.Add(reader.GetString("categoryName"));
                }
            }
        }

        private void orderItemsBtn_Click(object sender, EventArgs e)
        {
            Dictionary<Product,int> products= new Dictionary<Product,int>();
            int totalPrice = 0;
            int userId;
            int invoiceID;
            int invoiceItemsID;
            foreach (ListViewItem li in invoiceItems.Items)
            {
                products[storageItems.Find(p => p.name == li.SubItems[0].Text)] = int.Parse(li.SubItems[2].Text);
                totalPrice += int.Parse(li.SubItems[1].Text);
            }
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"select idUser from admin where username=@name";
            command.Parameters.AddWithValue("@name", Program.currentUser.username);
            using(MySqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                userId = reader.GetInt32("idUser");
                reader.Close();
                con.Close();
            }
            invoiceID = InvoiceCreation(totalPrice, userId);
            invoiceItemsID = InvoiceItemsCreation(totalPrice, invoiceID);
            UpdateProducts(products, invoiceItemsID);
            MessageBox.Show("Invoice completed!","Success!",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private int InvoiceCreation(int totalPrice,int adminID)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"insert into invoice(name, surname, number, city, totalPrice, orderDate, adminID)
                                                 values(@paramname,@paramsurname,@paramnumber,@paramcity,@paramptotalPrice,@paramorderDate,@paramadminID); select last_insert_id() ";
            command.Parameters.AddWithValue("@paramname", Program.currentUser.username);
            command.Parameters.AddWithValue("@paramsurname", Program.currentUser.surname);
            command.Parameters.AddWithValue("@paramnumber", Program.currentUser.phoneNumber);
            command.Parameters.AddWithValue("@paramcity", Program.currentUser.city);
            command.Parameters.AddWithValue("@paramptotalPrice", totalPrice);
            command.Parameters.AddWithValue("@paramorderDate", DateTime.Now);
            command.Parameters.AddWithValue("@paramadminID", adminID);
            return Convert.ToInt32(command.ExecuteScalar());
        }
        private int InvoiceItemsCreation(int totalPrice,int invoiceID) 
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = @"insert into `invoice items`(totalPrice, invoiceID) values (@paramtotalPrice,@paraminvoiceID); select last_insert_id()";
            command.Parameters.AddWithValue("paramtotalPrice",totalPrice);
            command.Parameters.AddWithValue("paraminvoiceID", invoiceID);
            return Convert.ToInt32(command.ExecuteScalar());
        }
        private void UpdateProducts(Dictionary<Product,int> products,int invoiceItemsID)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            foreach (Product p in storageItems)
            {
                if (products.ContainsKey(p)) 
                {
                    command.CommandText = @"update product set quantity=@newQuantity, invoiceItemsID=@invoiceID where name=@productName";
                    command.Parameters.AddWithValue("@newQuantity",(p.quantity + products[p]).ToString());
                    command.Parameters.AddWithValue("@invoiceID", invoiceItemsID);
                    command.Parameters.AddWithValue("@productName", p.name);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }
    }
}
