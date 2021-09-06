using Microsoft.AspNetCore.Identity;

namespace PWApplication.Models
{
    public class User : IdentityUser
    {
        public decimal Balance { get; set; }
    }
}
