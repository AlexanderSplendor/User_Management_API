using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace User_Management_API.Dtos
{
    public class UserCreateDto
    {
        
        public required string Username { get; set; }

        
        public required string Email { get; set; }

        
        public required string Password { get; set; }
}
}
