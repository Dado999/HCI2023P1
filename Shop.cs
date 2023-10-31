using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damir_Filipovic_HCI2023
{
    public partial class Shop : Form
    {
        bool componentsCollapsed=true;
        bool peripheralsCollapsed=true;
        bool storageCollapsed = true;

        public Shop()
        {
            InitializeComponent();
            Program.UpdateTheme(this);
        }

        //Collapsing animations
        private void componentsButton_Click(object sender, EventArgs e) { componentsTimer.Start(); }
        private void peripheralsBtn_Click(object sender, EventArgs e) { peripheralsTimer.Start(); }
        private void storageBtn_Click(object sender, EventArgs e) { storageTimer.Start(); }
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

        }
        private void componentsCpuBtn_Click(object sender, EventArgs e)
        {

        }
        private void componentsRAMBtn_Click(object sender, EventArgs e)
        {

        }
        private void componentMotherboardBtn_Click(object sender, EventArgs e)
        {

        }
        private void componentPSBtn_Click(object sender, EventArgs e)
        {

        }

        //Submenu button click - Peripherals
        private void peripheralsMouseBtn_Click(object sender, EventArgs e)
        {

        }
        private void peripheralsKeyboardBtn_Click(object sender, EventArgs e)
        {

        }
        private void peripheralsSpeakersBtn_Click(object sender, EventArgs e)
        {

        }
        private void peripheralsHeadphonesBtn_Click(object sender, EventArgs e)
        {

        }

        //Submenu button click - Storage
        private void storageHDDBtn_Click(object sender, EventArgs e)
        {

        }
        private void storageSSDBtn_Click(object sender, EventArgs e)
        {

        }
    }

}
