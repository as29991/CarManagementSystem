using CarManagementSystem.Models.Identity;

namespace CarManagementSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string UserId { get; set; }  // Matches the type of IdentityUser.Id
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }

        // Navigation properties to link to the corresponding Vehicle and ApplicationUser
        public Vehicle Vehicle { get; set; }
        public ApplicationUser User { get; set; }
    }
}
