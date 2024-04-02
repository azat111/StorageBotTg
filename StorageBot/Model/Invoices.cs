using System;
namespace StorageBot.Model
{
    public partial class Invoices
    {
        public int InvoiceId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? SupplierId { get; set; }
        public decimal? TotalCost { get; set; }
        public string InvoiceNumber { get; set; }

        public virtual Suppliers Supplier { get; set; }
    }
}
