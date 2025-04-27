using EZFIN_PROJECT.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EZFIN_PROJECT.Data
{
    public class FinanceContext : IdentityDbContext<User>
    {
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) { }

        public DbSet<SavingPlan> SavingPlans { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Revenue> Revenues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User to SavingPlan relationship
            modelBuilder.Entity<SavingPlan>()
                .HasOne(sp => sp.User)
                .WithMany(u => u.SavingPlans)
                .HasForeignKey(sp => sp.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // User to Transaction relationship
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Transaction to Expense relationship (optional relationship)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Expense)
                .WithOne(e => e.Transaction)
                .HasForeignKey<Expense>(e => e.TransactionID)
                .OnDelete(DeleteBehavior.SetNull);  // Optional relationship

            // Transaction to Revenue relationship (optional relationship)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Revenue)
                .WithOne(r => r.Transaction)
                .HasForeignKey<Revenue>(r => r.TransactionID)
                .OnDelete(DeleteBehavior.SetNull);  // Optional relationship
        }
    }
}