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
                    MessageBox.Show("Successful registration", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Shop shp = new Shop();
                    shp.Show();
                    this.Close();
                }

            }
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
            new StartPage().Show();
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
            return 1;
        }
    }
}
