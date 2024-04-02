using System;
using System.Collections.Generic;

namespace StorageBot.Model
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            WarehouseProduct = new HashSet<WarehouseProduct>();
        }

        public int WarehouseId { get; set; }
        public DateTime? DateAdded { get; set; }

        public virtual ICollection<WarehouseProduct> WarehouseProduct { get; set; }
    }
}
