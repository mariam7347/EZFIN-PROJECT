
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace EZFIN_PROJECT.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string BusinessType { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal TotalRevenue { get; set; }  
        public decimal TotalExpenses { get; set; }

        // Navigation properties
        public ICollection<SavingPlan> SavingPlans { get; set; } = new List<SavingPlan>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public ICollection<Revenue> Revenues { get; set; } = new List<Revenue>();  // Associated revenues
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();  // Associated expenses
    }
}
