using System.ComponentModel.DataAnnotations;

namespace e_commerce.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
