using System.ComponentModel.DataAnnotations;

namespace CarManagementSystem.DTOs
{
    public class RegistrationRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
    }
}
