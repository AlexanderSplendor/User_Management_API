using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Management_API.Data;
using User_Management_API.Dtos;
using User_Management_API.Helpers;
using User_Management_API.Models;

namespace User_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/users - Create a new user and bulk users Creation
        [HttpPost]
        public async Task<IActionResult> CreateUsers([FromBody] List<UserCreateDto> users)
        {
            if (users == null || users.Count == 0)
                return BadRequest("User list cannot be empty.");

            var createdUsers = new List<object>();

            foreach (var dto in users)
            {
                // Validate password
                if (!PasswordHelper.IsStrongPassword(dto.Password))
                    return BadRequest($"Weak password for user: {dto.Username}");

                // Check for duplicate username or email
                bool userExists = await _context.Users.AnyAsync(u => u.Username == dto.Username || u.Email == dto.Email);
                if (userExists)
                    return BadRequest($"Username or Email already exists: {dto.Username}, {dto.Email}");

                var user = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    PasswordHash = PasswordHelper.HashPassword(dto.Password)
                };

                _context.Users.Add(user);
                createdUsers.Add(new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    user.CreatedAt
                });
            }

            await _context.SaveChangesAsync();

            return StatusCode(201, createdUsers);
        }

        // POST /api/users - Create a single user (for testing)
        //public async Task<IActionResult> CreateUser(UserCreateDto dto)
        //{
        //    // Validate password strength
        //    if (!PasswordHelper.IsStrongPassword(dto.Password))
        //    {
        //        return BadRequest("Password must be at least 8 characters and contain a special character.");
        //    }

        //    // Check for existing user/email
        //    bool userExists = await _context.Users.AnyAsync(u =>
        //        u.Username == dto.Username || u.Email == dto.Email);

        //    if (userExists)
        //    {
        //        return BadRequest("Username or Email already exists.");
        //    }

        //    var user = new User
        //    {
        //        Username = dto.Username,
        //        Email = dto.Email,
        //        PasswordHash = PasswordHelper.HashPassword(dto.Password)
        //    };

        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return StatusCode(201, new { user.Id, user.Username, user.Email, user.CreatedAt });
        //}



        // GET /api/users/{id} - Get user profile
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var response = new
            {
                user.Id,
                user.Username,
                user.Email,
                user.IsAdmin,
                user.CreatedAt
            };

            return Ok(response);
        }



        // PUT /api/users/{id} - Update user info (username, email)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserInfo(Guid id, UpdateUserInfoDto dto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Check if the new username or email already exists
            bool userExists = await _context.Users.AnyAsync(u =>
                (u.Username == dto.Username || u.Email == dto.Email) && u.Id != id);

            if (userExists)
            {
                return BadRequest("Username or Email already taken.");
            }

            // Update the user info
            user.Username = dto.Username;
            user.Email = dto.Email;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var response = new
            {
                user.Id,
                user.Username,
                user.Email,
                user.IsAdmin,
                user.CreatedAt
            };

            return Ok(response);
        }



        // DELETE /api/users/{id} - Delete user (simulate current user)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            // Simulate logged-in user
            Guid currentUserId = Guid.Parse("11111111-1111-1111-1111-111111111111"); // Just hardcoded for now

            if (id == currentUserId)
            {
                return Forbid("You cannot delete yourself.");
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }


    }
}
    
