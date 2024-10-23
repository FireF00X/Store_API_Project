using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTOs.AccountUsersDto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
