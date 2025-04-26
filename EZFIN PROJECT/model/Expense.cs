using System;

namespace EZFIN_PROJECT.Model
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        public int TransactionID { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } // Example values: "Paid", "Unpaid"
        public string Description { get; set; }

        // Navigation property
        public Transaction Transaction { get; set; }

        // Future feature: Alerts related to this Expense
        // public ICollection<Alert> Alerts { get; set; }
    }
}
