using System.ComponentModel.DataAnnotations;

namespace User_Management_API.Dtos
{
    public class LoginDto
    {
        
        public required string Username { get; set; }

       
        public required string Password { get; set; }

    }
}
