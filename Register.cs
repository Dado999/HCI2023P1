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
using System.Xml.Linq;

namespace Damir_Filipovic_HCI2023
{
    public partial class Register : Form
    {

        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HCI"].ConnectionString;
        public Register()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameLabel.Text) || string.IsNullOrEmpty(surnameLabel.Text) || string.IsNullOrEmpty(usernameRegLabel.Text) ||
                string.IsNullOrEmpty(passwordRegLabel.Text) || string.IsNullOrEmpty(numberLabel.Text) || string.IsNullOrEmpty(cityLabel.Text))
            {
                MessageBox.Show("Please fill out all the boxes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (createUser() == 1)
                {
                    MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                    mySqlConnection.Open();
                    MySqlCommand command = mySqlConnection.CreateCommand();
                    command.CommandText = @"SELECT * FROM city WHERE name=@city";
                    command.Parameters.AddWithValue("@city", cityLabel.Text);
                    if (command.ExecuteNonQuery() < 0)
                    {
                        command.CommandText = @"INSERT INTO city(name) values (@name)";
                        command.Parameters.AddWithValue("@name", cityLabel.Text);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Successful registration", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Program.currentUser = new User(nameLabel.Text, surnameLabel.Text, usernameRegLabel.Text, passwordRegLabel.Text, numberLabel.Text, cityLabel.Text,"English","Light");
                    Shop shp = new Shop();
                    shp.Show();
                    this.Hide();
                }

            }
        }
        private int createUser()
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand mySqlCommand = conn.CreateCommand();
            mySqlCommand.CommandText = @"INSERT INTO user(name,surname,username,password,phoneNumber,city,language,theme) VALUES (@name,@surname,@username,@password,@phoneNumber,@city,@language,@theme)";
            mySqlCommand.Parameters.AddWithValue("@name", nameLabel.Text);
            mySqlCommand.Parameters.AddWithValue("@surname", surnameLabel.Text);
            mySqlCommand.Parameters.AddWithValue("@username", usernameRegLabel.Text);
            mySqlCommand.Parameters.AddWithValue("@password", passwordRegLabel.Text);
            mySqlCommand.Parameters.AddWithValue("@phoneNumber", numberLabel.Text);
            mySqlCommand.Parameters.AddWithValue("@city", cityLabel.Text);
            mySqlCommand.Parameters.AddWithValue("@language", "English");
            mySqlCommand.Parameters.AddWithValue("@theme", "Light");
            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch(MySqlException ex) 
            { 
                MessageBox.Show("Username already exists\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            conn.Close();
            return 1;
        }

        private void Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            new StartPage().Show();
        }
    }
}
