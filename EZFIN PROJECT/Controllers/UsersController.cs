using Microsoft.AspNetCore.Mvc;
using EZFIN_PROJECT.Services;  // Ensure correct namespace for UserService
using EZFIN_PROJECT.Model;     // Ensure correct namespace for User model

namespace EZFIN_PROJECT.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        // Constructor injection for UserService
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // POST: Register a new user
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Register the user using UserService
                bool result = _userService.RegisterUser(user);

                if (result)
                {
                    // Redirect to login page after successful registration
                    return RedirectToAction("Login");
                }
                else
                {
                    // If the user already exists, show an error message
                    ModelState.AddModelError("", "A user with this email already exists.");
                    return View(user); // Return the same view with an error message
                }
            }

            // If model state is not valid, return the same view with validation errors
            return View(user);
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // If email or password is empty, show an error message
                ModelState.AddModelError("", "Email and password are required.");
                return View();
            }

            var user = _userService.GetUserByEmail(email); // Retrieve user by email

            if (user != null && _userService.ValidateUserPassword(user, password)) // Validate password
            {
                // If valid, redirect to dashboard or another page
                return RedirectToAction("Dashboard");
            }

            // If validation fails, show an error message
            ModelState.AddModelError("", "Invalid login credentials.");
            return View();
        }

        // POST: Add revenue or expense
        [HttpPost]
        public IActionResult AddTransaction(string userId, decimal amount, string type, string description)
        {
            try
            {
                // Call the AddTransaction method from UserService
                _userService.AddTransaction(userId, amount, type, description);

                // Redirect to dashboard after the transaction is successfully added
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                // If there is an error, add the error message to the model and return to the same view
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}