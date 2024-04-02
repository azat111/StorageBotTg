using System;
using System.Collections.Generic;
namespace StorageBot.Model
{
    public partial class WarehouseProduct
    {
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Products Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
