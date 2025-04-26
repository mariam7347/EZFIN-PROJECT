using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EZFIN_PROJECT.Model;
using Microsoft.EntityFrameworkCore;

namespace EZFIN_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // ✅ Get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        // ✅ Get user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // ✅ Create new user (basic)
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var result = await _userManager.CreateAsync(user, "DefaultPassword@123"); // 🛑 Default password

            if (result.Succeeded)
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);

            return BadRequest(result.Errors);
        }

        // ✅ Update user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, User updatedUser)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.BusinessType = updatedUser.BusinessType;
            user.Email = updatedUser.Email;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.EnrollmentDate = updatedUser.EnrollmentDate;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Errors);
        }

        // ✅ Delete user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Errors);
        }
    }
}
