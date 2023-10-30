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
    public partial class Settings : Form
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["HCI"].ConnectionString;

        public Settings()
        {
            InitializeComponent();
            Program.UpdateTheme(this);
        }

        private void lightThemePicture_MouseHover(object sender, EventArgs e)
        {
            previewImage.Visible = true;
            previewImage.BackgroundImage = Properties.Resources.LightTheme;
        }

        private void lightThemePicture_MouseLeave(object sender, EventArgs e)
        {
            previewImage.BackgroundImage = null;
            previewImage.Visible = false;
        }

        private void darkThemePicture_MouseHover(object sender, EventArgs e)
        {
            previewImage.Visible = true;
            previewImage.BackgroundImage = Properties.Resources.DarkTheme;
        }

        private void darkThemePicture_MouseLeave(object sender, EventArgs e)
        {
            previewImage.BackgroundImage = null;
            previewImage.Visible = false;
        }

        private void contrastThemePicture_MouseHover(object sender, EventArgs e)
        {
            previewImage.Visible = true;
            previewImage.BackgroundImage = Properties.Resources.ContrastTheme;
        }

        private void contrastThemePicture_MouseLeave(object sender, EventArgs e)
        {
            previewImage.BackgroundImage = null;
            previewImage.Visible = false;
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            new Shop().Show();
        }

        private void lightThemePicture_Click(object sender, EventArgs e)
        {
            changeTheme("Light");
            Program.UpdateTheme(this);
        }

        private void darkThemePicture_Click(object sender, EventArgs e)
        {
            changeTheme("Dark");
            Program.UpdateTheme(this);
        }

        private void contrastThemePicture_Click(object sender, EventArgs e)
        {
            changeTheme("Contrast");
            Program.UpdateTheme(this);
        }
        private void changeTheme(string newTheme)
        {
            Program.currentUser.theme = newTheme;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = @"UPDATE user SET theme=@theme WHERE username=@username";
            mySqlCommand.Parameters.AddWithValue("@username", Program.currentUser.username);
            mySqlCommand.Parameters.AddWithValue("@theme", newTheme);
            try { mySqlCommand.ExecuteNonQuery(); }
            catch (MySqlException ex) { MessageBox.Show(ex.Message); }
        }
    }
}
