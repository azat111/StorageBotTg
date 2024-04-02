using System;
using System.Collections.Generic;
namespace StorageBot.Model
{
    public partial class Orders
    {
        public Orders()
        {
            OrdersProduct = new HashSet<OrdersProduct>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? IdStatus { get; set; }
        public int? CustomerId { get; set; }
        public int? OrderCost { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual StatusTypes IdStatusNavigation { get; set; }
        public virtual ICollection<OrdersProduct> OrdersProduct { get; set; }
    }
}
