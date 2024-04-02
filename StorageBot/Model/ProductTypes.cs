using System;
using System.Collections.Generic;
namespace StorageBot.Model
{
    public partial class ProductTypes
    {
        public ProductTypes()
        {
            Products = new HashSet<Products>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
