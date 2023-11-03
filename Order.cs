using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Damir_Filipovic_HCI2023
{
    public partial class Order : Form
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HCI"].ConnectionString;
        private int price;
        private int adressId;
        private int cityId;
        private int userId;
        private int orderId;
        private int orderedItemsId;
        public Order(int price)
        {
            InitializeComponent();
            Program.UpdateTheme(this);
            nameLabel.Text = Program.currentUser.name.ToString();
            surnameLabel.Text = Program.currentUser.surname.ToString();
            numberLabel.Text = Program.currentUser.phoneNumber.ToString();
            cityLabel.Text = Program.currentUser.city.ToString();
            priceLabel.Text = "Total price: " + price.ToString() + "KM";
            this.price = price;
        }

        private void adressLabel_Click(object sender, EventArgs e)
        {
            adressLabel.Text = "";
        }
        private void orderButton_Click_1(object sender, EventArgs e)
        {
            adressId = GetAddressID(adressLabel.Text);
            userId = GetUserId(Program.currentUser.username);
            createOrder();
            createOrderedItems();
            removeItems();
        }
        private int GetAddressID(string address)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand mysqlcommand = conn.CreateCommand())
                {
                    mysqlcommand.CommandText = "SELECT idCity FROM city WHERE name = @CityName";
                    mysqlcommand.Parameters.AddWithValue("@CityName", cityLabel.Text);
                    using (MySqlDataReader reader = mysqlcommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cityId = reader.GetInt32(0);
                        }
                    }
                    mysqlcommand.Parameters.Clear();
                    mysqlcommand.CommandText = "SELECT idAdress FROM adress WHERE name = @AddressName";
                    mysqlcommand.Parameters.AddWithValue("@AddressName", address);

                    object result = mysqlcommand.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    mysqlcommand.Parameters.Clear();
                    mysqlcommand.CommandText = "INSERT INTO adress (name, cityID) VALUES (@AddressName, @CityID)";
                    mysqlcommand.Parameters.AddWithValue("@AddressName", address);
                    mysqlcommand.Parameters.AddWithValue("@CityID", cityId);

                    if (mysqlcommand.ExecuteNonQuery() > 0)
                    {
                        mysqlcommand.Parameters.Clear();
                        mysqlcommand.CommandText = "SELECT LAST_INSERT_ID()";
                        return Convert.ToInt32(mysqlcommand.ExecuteScalar());
                    }
                }
            }
            return 0;
        }
        private int GetUserId(string userName)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = @"select idUser from user where username=@userName";
                command.Parameters.AddWithValue("@userName", userName);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        private void createOrder()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into hci.order(name,surname,number,city,totalPrice,orderDate,userID,adressID)
                                                       values(@Name,@Surname,@Number,@City,@price,@date,@user,@adress)";
                command.Parameters.AddWithValue("@Name", Program.currentUser.name);
                command.Parameters.AddWithValue("@Surname", Program.currentUser.surname);
                command.Parameters.AddWithValue("@Number", Program.currentUser.phoneNumber);
                command.Parameters.AddWithValue("@City", Program.currentUser.city);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.Parameters.AddWithValue("@user", userId);
                command.Parameters.AddWithValue("@adress", adressId);

                command.ExecuteNonQuery();

                command.Parameters.Clear();
                command.CommandText = "SELECT LAST_INSERT_ID()";
                orderId = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        private void createOrderedItems()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText = @"insert into `ordered items` (totalprice,orderID) values (@price,@ordrId)";
                mySqlCommand.Parameters.AddWithValue("@price", price);
                mySqlCommand.Parameters.AddWithValue("@ordrId", orderId);
                mySqlCommand.ExecuteNonQuery();
                mySqlCommand.Parameters.Clear();
                mySqlCommand.CommandText = "select last_insert_id()";
                orderedItemsId = Convert.ToInt32(mySqlCommand.ExecuteScalar());
            }
        }
        private void removeItems()
        {
            foreach (Product p in Shop.clickedProducts)
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    MySqlCommand com = con.CreateCommand();
                    com.CommandText = @"update product set quantity=@newQuantity, orderedItemsID=@newOIID where name=@productName";
                    com.Parameters.AddWithValue("@newQuantity", p.quantity - p.clickLimit);
                    com.Parameters.AddWithValue("@newOIID", orderedItemsId);
                    com.Parameters.AddWithValue("@productName", p.name);
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
