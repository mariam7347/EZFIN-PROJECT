using BCrypt.Net;
using EZFIN_PROJECT.Data;  // Change this to your correct namespace for FinanceContext
using EZFIN_PROJECT.Model;

namespace EZFIN_PROJECT.Services
{
    public class UserService
    {
        private readonly FinanceContext _context;

        public UserService(FinanceContext context)
        {
            _context = context;
        }

        // Register a new user
        public bool RegisterUser(User user)
        {
            // Check if the user already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return false; // Return false if user with this email already exists
            }

            // Hash the password (use raw password, not PasswordHash)
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            var newUser = new User
            {
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BusinessType = user.BusinessType,
                PhoneNumber = user.PhoneNumber,
                EnrollmentDate = user.EnrollmentDate,
                PasswordHash = hashedPassword // Store the hashed password in PasswordHash
            };

            // Save the newUser to the database
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return true; // Return true if the user is successfully registered
        }

        // Validate user password
        public bool ValidateUserPassword(User user, string enteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, user.PasswordHash); // Validate the entered password
        }

        // Get user by email
        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email); // Retrieve user by email
        }
        // Add revenue or expense and update totals
        public void AddTransaction(string userId, decimal amount, string type, string description)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var transaction = new Transaction
            {
                UserID = userId,
                Amount = amount,
                Date = DateTime.Now,
                Type = type // "Revenue" or "Expense"
            };

            if (type == "Revenue")
            {
                var revenue = new Revenue
                {
                    Amount = amount,
                    DueDate = DateTime.Now.AddMonths(1), // Example: Revenue due in 1 month
                    Status = "Unpaid",
                    Description = description,
                    Transaction = transaction
                };

                // Update the total revenue for the user
                user.TotalRevenue += amount;

                _context.Revenues.Add(revenue);
            }
            else if (type == "Expense")
            {
                var expense = new Expense
                {
                    Amount = amount,
                    DueDate = DateTime.Now.AddMonths(1), // Example: Expense due in 1 month
                    Status = "Unpaid",
                    Description = description,
                    Transaction = transaction
                };

                // Update the total expenses for the user
                user.TotalExpenses += amount;

                _context.Expenses.Add(expense);
            }
            else
            {
                throw new Exception("Invalid transaction type.");
            }

            // Add the transaction to the database
            _context.Transactions.Add(transaction);

            // Save all changes to the database
            _context.SaveChanges();
        }
    }
}