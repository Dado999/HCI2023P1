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

namespace Damir_Filipovic_HCI2023
{
    public partial class AdminSystem : Form
    {
        BindingList<User> users = new BindingList<User>();
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HCI"].ConnectionString;
        DataGridView dgv = new DataGridView();
        public AdminSystem()
        {
            InitializeComponent();
            ShowAllUsers();
        }
        private void ShowAllUsers()
        {   
            dgv = CreateTable();
            GetUsers();
            dgv.DataSource = users;
            panel1.Controls.Add(dgv);
        }
        private DataGridView CreateTable()
        {
            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgv.BringToFront();
            dgv.AllowUserToDeleteRows = true;
            return dgv;
        }
        private void GetUsers() 
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from user";

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(new User(
                        reader.GetString("name"),
                        reader.GetString("surname"),
                        reader.GetString("username"),
                        reader.GetString("password"),
                        reader.GetString("phoneNumber"),
                        reader.GetString("city"),
                        reader.GetString("language"),
                        reader.GetString("theme")
                        ));
                }
            }
        }

        private void deleteUserBtn_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv.SelectedRows[0];
                int rowIndex = dgv.CurrentCell.RowIndex;
                dgv.Rows.RemoveAt(rowIndex);
                users.RemoveAt(rowIndex);
            }
        }
    }
}
