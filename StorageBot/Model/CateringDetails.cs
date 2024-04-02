using System;
using System.Collections.Generic;

namespace StorageBot.Model
{
    public partial class CateringDetails
    {
        public int CateringId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? Cost { get; set; }

        public virtual Catering Catering { get; set; }
        public virtual Products Product { get; set; }
    }
}
