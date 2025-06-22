using System.ComponentModel.DataAnnotations;

namespace CarManagementSystem.Models
{

    public enum VehicleStatus
    {
        Available,
        Sold
    }

    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public VehicleStatus Status { get; set; }

    }
}
