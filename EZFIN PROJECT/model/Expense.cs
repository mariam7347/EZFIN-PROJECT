using System.ComponentModel.DataAnnotations;

namespace EZFIN_PROJECT.Model
{
    public class Expense
    {
        [Key]
        public int ExpenseID { get; set; }

        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        // Foreign key to Transaction
        public int? TransactionID { get; set; }  // <- Make it nullable
        public Transaction Transaction { get; set; }
    }
}