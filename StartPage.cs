using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;

namespace Damir_Filipovic_HCI2023
{
    public partial class StartPage : Form
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HCI"].ConnectionString;
        public StartPage()
        {
            InitializeComponent();
        }
        private void registerButton_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Hide();
        }
        private void logInButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usernameField.Text) || string.IsNullOrEmpty(passwordField.Text))
                MessageBox.Show("Fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                MySqlConnection con = new MySqlConnection(connectionString);
                con.Open();
                MySqlCommand mySqlCommand = con.CreateCommand();
                mySqlCommand.CommandText = @"SELECT * FROM user WHERE username=@username";
                mySqlCommand.Parameters.AddWithValue("@username", usernameField.Text);

                try
                {
                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString("name");
                            string surname = reader.GetString("surname");
                            string username = reader.GetString("username");
                            string password = reader.GetString("password");
                            string phoneNumber = reader.GetString("phoneNumber");
                            string city = reader.GetString("city");
                            string language = reader.GetString("language");
                            string theme = reader.GetString("theme");

                            if (username == usernameField.Text && password == passwordField.Text)
                            {
                                Program.currentUser = new User(name, surname, username, password, phoneNumber, city, language, theme);
                                Shop shp = new Shop();
                                shp.Show();
                                this.Hide();
                            }
                            else if (password != passwordField.Text)
                            {
                                MessageBox.Show("Wrong password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            reader.Close(); // Close the previous reader

                            MySqlCommand adminCommand = (MySqlCommand)mySqlCommand.Clone(); // Create a new command with the same connection
                            adminCommand.CommandText = @"select * from admin where username=@user";
                            adminCommand.Parameters.AddWithValue("@user", usernameField.Text);

                            using (MySqlDataReader rdr = adminCommand.ExecuteReader())
                            {
                                if (rdr.Read())
                                {
                                    if (rdr.GetInt32("adminSales") == 1 && rdr.GetString("password").Equals(passwordField.Text))
                                    {
                                        new AdminSales().Show();
                                        Program.currentUser = new User(
                                            rdr.GetString("name"), 
                                            rdr.GetString("surname"),
                                            rdr.GetString("username"),
                                            rdr.GetString("password"),
                                            rdr.GetString("phoneNumber"),
                                            rdr.GetString("city"),
                                            rdr.GetString("language"),
                                            rdr.GetString("theme"));
                                    }
                                    else if (rdr.GetInt32("adminSales") == 0 && rdr.GetString("password").Equals(passwordField.Text))
                                    {
                                        new AdminSystem().Show();
                                        Program.currentUser = new User(
                                            rdr.GetString("name"),
                                            rdr.GetString("surname"),
                                            rdr.GetString("username"),
                                            rdr.GetString("password"),
                                            rdr.GetString("phoneNumber"),
                                            rdr.GetString("city"),
                                            rdr.GetString("language"),
                                            rdr.GetString("theme"));
                                    }
                                    else
                                    {
                                        MessageBox.Show("User not found, or incorrect password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }

        }
    }
}
