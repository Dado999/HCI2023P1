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
    public partial class AdminSystem : Form
    {
        BindingList<User> users = new BindingList<User>();
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HCI"].ConnectionString;
        DataGridView dgv = new DataGridView();
        public AdminSystem()
        {
            InitializeComponent();
            ShowAllUsers();
            adminComboBox.Items.Add("Sales");
            adminComboBox.Items.Add("System");
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
                    if(reader.GetInt16("userActive")==1)
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
                string userName = selectedRow.Cells[2].Value.ToString();
                DialogResult dr = MessageBox.Show("Are you sure you want to delete the user " + userName, "?", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    dgv.Rows.RemoveAt(rowIndex);
                    users.RemoveAt(rowIndex == 0 ? 0 : rowIndex-1);
                    RemoveUser(userName);   
                }
            }
        }
        private void RemoveUser(string userName) 
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"update user set userActive=0 where username=@userName";
            cmd.Parameters.AddWithValue("@username", userName);
            cmd.ExecuteNonQuery();
            MessageBox.Show("User " + userName + " deleted successfully","Successful deletion",MessageBoxButtons.OK);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nameTB.Text.Length == 0 || surnameTB.Text.Length == 0 || userNameTB.Text.Length == 0 || passwordTB.Text.Length == 0
                || phoneTB.Text.Length == 0 || cityTB.Text.Length == 0 || adminComboBox.Text == "Role")
                MessageBox.Show("Fill in all of the fields!", "Error", MessageBoxButtons.OK);
            else
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into hci.admin(name, surname, username, password, phoneNumber, city, language, theme, adminSales)
                                                    values(@parname,@parsurname,@parusername,@parpassword,@parphoneNumber,@parcity,'English','Light',@parrole)";
                cmd.Parameters.AddWithValue("@parname",nameTB.Text);
                cmd.Parameters.AddWithValue("@parsurname",surnameTB.Text);
                cmd.Parameters.AddWithValue("@parusername",userNameTB.Text);
                cmd.Parameters.AddWithValue("@parpassword",passwordTB.Text);
                cmd.Parameters.AddWithValue("@parphoneNumber",phoneTB.Text);
                cmd.Parameters.AddWithValue("@parcity",cityTB.Text);
                cmd.Parameters.AddWithValue("@parrole",adminComboBox.Text == "Sales" ? 1 : 0);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Admin " + userNameTB.Text + " deleted successfully", "Successful creation", MessageBoxButtons.OK);
            }
        }
    }
}
