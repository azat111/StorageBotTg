using System;
using System.Collections.Generic;
namespace StorageBot.Model
{
    public partial class Catering
    {
        public Catering()
        {
            CateringDetails = new HashSet<CateringDetails>();
        }

        public int CateringId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? EventDate { get; set; }
        public int? TotalCost { get; set; }
        public int? CustomerId { get; set; }
        public int? IdStatus { get; set; }
        public string EventAddress { get; set; }
        public string Link { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual StatusTypes IdStatusNavigation { get; set; }
        public virtual ICollection<CateringDetails> CateringDetails { get; set; }
    }
}
