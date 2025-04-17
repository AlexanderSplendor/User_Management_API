using System.ComponentModel.DataAnnotations;

namespace User_Management_API.Dtos
{
    public class UpdateUserInfoDto
    {
        [Required]
        [StringLength(100)]
        public required string Username { get; set; }

        public required string Email { get; set; }
    }
}
