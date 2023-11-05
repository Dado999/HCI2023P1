using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damir_Filipovic_HCI2023
{
    public partial class AdminSales : Form
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HCI"].ConnectionString;
        List<databaseOrder> databaseOrders = new List<databaseOrder>();
        public AdminSales()
        {
            InitializeComponent();
        }

        private void ordersBtn_Click(object sender, EventArgs e)
        {
            /*ordersTable.Visible = true;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from hci.order";

            using(MySqlDataReader reader = cmd.ExecuteReader())
            {
                int i = 0;
                while (reader.Read())
                {
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
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(ordersTable);
                    row.Cells["idOrder"].Value = reader.GetInt32("idOrder");
                    row.Cells["name"].Value = reader.GetString("name");
                    row.Cells["surname"].Value = reader.GetString("surname");
                    row.Cells["number"].Value = reader.GetString("number");
                    row.Cells["city"].Value = reader.GetString("city");
                    row.Cells["totalPrice"].Value = reader.GetInt32("totalPrice");
                    row.Cells["orderDate"].Value = reader.GetString("orderDate");
                    row.Cells["userID"].Value = reader.GetInt32("userID");
                    row.Cells["adressID"].Value = reader.GetInt32("adressID");
                    ordersTable.Rows.Add(row);
                    i+=1; 
                }
            }*/
            using(MySqlConnection conn = new MySqlConnection(connectionString))
            { 
                conn.Open();
                string query = "select * from hci.order";
                SqlDataAdapter adapter = new SqlDataAdapter(query,connectionString);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ordersTable.DataSource = dt;
            }
        }
    }
}
