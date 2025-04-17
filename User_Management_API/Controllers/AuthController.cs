using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Management_API.Data;
using User_Management_API.Dtos;
using User_Management_API.Helpers;

namespace User_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var hashedInputPassword = PasswordHelper.HashPassword(dto.Password);

            if (user.PasswordHash != hashedInputPassword)
            {
                return Unauthorized("Invalid username or password.");
            }

            var response = new
            {
                user.Id,
                user.Username,
                user.Email,
                user.IsAdmin,
                user.CreatedAt,
                token = "abc123" // Dummy token
            };

            return Ok("Login Successful");
        }
    }
}
