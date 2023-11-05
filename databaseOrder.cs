using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damir_Filipovic_HCI2023
{
    internal class databaseOrder
    {
        public int idOrder { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string number { get; set; }
        public string city { get; set; }
        public int totalPrice { get; set; }
        public string orderDate { get; set; }
        public int userID { get; set; }
        public int adressID { get; set; }

        public databaseOrder(int idOrder, string name, string surname, string number, string city, int totalPrice, string orderDate, int userID, int adressID)
        {
            this.idOrder = idOrder;
            this.name = name;
            this.surname = surname;
            this.number = number;
            this.city = city;
            this.totalPrice = totalPrice;
            this.orderDate = orderDate;
            this.userID = userID;
            this.adressID = adressID;
        }
    }
}
