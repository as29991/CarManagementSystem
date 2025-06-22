using CarManagementSystem.Models;

namespace CarManagementSystem.DTOs
{
    public class CreateVehicleDTO
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public VehicleStatus Status { get; set; }

    }
}
