namespace CarManagementSystem.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string UserId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
    }
}
