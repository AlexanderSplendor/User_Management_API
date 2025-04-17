using System.ComponentModel.DataAnnotations;

namespace User_Management_API.Models
{
    public class User
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();

        
        public required string Username { get; set; }

       
        public required string Email { get; set; }

        
        public required string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsAdmin { get; set; } = false;
    }
}
