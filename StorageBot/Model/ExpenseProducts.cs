using System;
using System.Collections.Generic;
namespace StorageBot.Model
{
    public partial class ExpenseProducts
    {
        public int ExpenseId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? Cost { get; set; }

        public virtual ExpenseForms Expense { get; set; }
        public virtual Products Product { get; set; }
    }
}
