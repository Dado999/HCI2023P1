using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damir_Filipovic_HCI2023
{
    internal static class Program
    {
        public static User currentUser;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartPage());
        }
        public static void UpdateTheme(Form form)
        {
            if(currentUser.theme.Equals("Light"))
            {
                form.BackColor = Color.White;
                foreach (Control control in form.Controls)
                {
                    if(control is Button)
                    {
                        Button button = (Button)control;
                        button.BackColor = Color.White;
                        button.FlatAppearance.BorderColor = Color.FromArgb(30, 97, 170);
                        button.ForeColor = Color.FromArgb(30, 97, 170);
                    }
                    else if(control is Label)
                        control.ForeColor = Color.FromArgb(30, 97, 170); 
                    else if(control is SplitContainer)
                    {
                        SplitContainer splitContainer = (SplitContainer)control;
                        splitContainer.Panel1.BackColor = Color.FromArgb(30, 97, 170);
                        splitContainer.Panel2.BackColor = Color.White;
                    }
                }
            }
            else if(currentUser.theme.Equals("Dark"))
            {
                form.BackColor = Color.Silver;
                foreach (Control control in form.Controls)
                {
                    if (control is Button)
                    {
                        Button button = (Button)control;
                        button.BackColor = Color.White;
                        button.FlatAppearance.BorderColor = Color.Black;
                        button.ForeColor = Color.Black;
                    }
                    else if (control is Label)
                        control.ForeColor = Color.Black;
                    else if (control is SplitContainer)
                    {
                        SplitContainer splitContainer = (SplitContainer)control;
                        splitContainer.Panel1.BackColor = Color.Black;
                        splitContainer.Panel2.BackColor = Color.Silver;
                    }
                }
            }
            else
            {
                form.BackColor = Color.Red;
                foreach (Control control in form.Controls)
                {
                    if (control is Button)
                    {
                        Button button = (Button)control;
                        button.BackColor = Color.White;
                        button.FlatAppearance.BorderColor = Color.Red;
                        button.ForeColor = Color.Black;
                    }
                    else if (control is Label)
                        control.ForeColor = Color.Black;
                    else if (control is SplitContainer)
                    {
                        SplitContainer splitContainer = (SplitContainer)control;
                        splitContainer.Panel1.BackColor = Color.Red;
                        splitContainer.Panel2.BackColor = Color.Chartreuse;
                    }
                }
            }
        }
    }
}
