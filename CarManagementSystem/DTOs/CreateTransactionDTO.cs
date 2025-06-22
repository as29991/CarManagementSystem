namespace CarManagementSystem.DTOs
{
    public class CreateTransactionDTO
    {
        public int VehicleId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }

    }
}
