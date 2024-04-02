using System;
using System.Collections.Generic;
namespace StorageBot.Model
{
    public partial class ExpenseForms
    {
        public ExpenseForms()
        {
            ExpenseProducts = new HashSet<ExpenseProducts>();
        }

        public int ExpenseId { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public int? TotalCost { get; set; }

        public virtual ICollection<ExpenseProducts> ExpenseProducts { get; set; }
    }
}
