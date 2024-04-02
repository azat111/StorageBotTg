using System;
using System.Collections.Generic;

namespace StorageBot.Model
{
    public partial class Products
    {
        public Products()
        {
            CateringDetails = new HashSet<CateringDetails>();
            ExpenseProducts = new HashSet<ExpenseProducts>();
            OrdersProduct = new HashSet<OrdersProduct>();
            WarehouseProduct = new HashSet<WarehouseProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryId { get; set; }
        public int? TypeId { get; set; }

        public virtual ProductCategories Category { get; set; }
        public virtual ProductTypes Type { get; set; }
        public virtual ICollection<CateringDetails> CateringDetails { get; set; }
        public virtual ICollection<ExpenseProducts> ExpenseProducts { get; set; }
        public virtual ICollection<OrdersProduct> OrdersProduct { get; set; }
        public virtual ICollection<WarehouseProduct> WarehouseProduct { get; set; }
    }
}
