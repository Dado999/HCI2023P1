using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damir_Filipovic_HCI2023
{
    internal class User
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string username  { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string city { get; set; }
        public string language { get; set; }
        public string theme { get; set; }

        public User(string name, string surname, string username, string password, string phoneNumber, string city, string language, string theme)
        {
            this.name = name;
            this.surname = surname;
            this.username = username;
            this.password = password;
            this.phoneNumber = phoneNumber;
            this.city = city;
            this.language = language;
            this.theme = theme;
        }

    }
}
