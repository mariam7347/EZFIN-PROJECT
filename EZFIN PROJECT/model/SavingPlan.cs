using System;

namespace EZFIN_PROJECT.Model
{
    public class SavingPlan
    {
        public int SavingPlanID { get; set; }
        public string UserID { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal CurrentSaved { get; set; }
        public DateTime TargetDate { get; set; }
        public string Status { get; set; } // Example values: "Active", "Completed"

        // Navigation property
        public User User { get; set; }
    }
}
