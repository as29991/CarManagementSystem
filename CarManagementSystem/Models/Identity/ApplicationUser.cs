using Microsoft.AspNetCore.Identity;

namespace CarManagementSystem.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
