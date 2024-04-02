using System;
using System.Collections.Generic;
namespace StorageBot.Model
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            Invoices = new HashSet<Invoices>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Invoices> Invoices { get; set; }
    }
}
