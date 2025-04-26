using System;

namespace EZFIN_PROJECT.Model
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public string UserID { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // Example values: "Expense", "Revenue"
        public DateTime Date { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Expense Expense { get; set; }
        public Revenue Revenue { get; set; }
    }
}
