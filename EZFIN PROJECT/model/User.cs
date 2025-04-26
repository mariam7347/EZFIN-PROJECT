
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

        // Navigation properties
        public ICollection<SavingPlan> SavingPlans { get; set; } = new List<SavingPlan>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
