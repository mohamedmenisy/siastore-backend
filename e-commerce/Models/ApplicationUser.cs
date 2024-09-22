using Microsoft.AspNetCore.Identity;

namespace e_commerce.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
