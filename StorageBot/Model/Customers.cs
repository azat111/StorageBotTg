using System;
using System.Collections.Generic;
namespace StorageBot.Model
{
    public partial class Customers
    {
        public Customers()
        {
            Catering = new HashSet<Catering>();
            Orders = new HashSet<Orders>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Catering> Catering { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
