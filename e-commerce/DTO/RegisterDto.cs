using System.ComponentModel.DataAnnotations;

namespace e_commerce.DTO
{
    public class RegisterDto
    {
        [Required]
        public string Name {  get; set; }
        [Required]
        public string UserName {  get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
       [Compare("Password",ErrorMessage = "Not Matched")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
