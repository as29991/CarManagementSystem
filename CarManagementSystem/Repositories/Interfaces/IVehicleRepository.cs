using CarManagementSystem.DTOs;
using CarManagementSystem.Models;

namespace CarManagementSystem.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task AddVehicleAsync(Vehicle vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle, int id);
        Task DeleteVehicleAsync(int id);
        Task<IEnumerable<Vehicle>> GetVehiclesByModelAsync(string model);
        Task<IEnumerable<Vehicle>> GetVehiclesByBrandAsync(string brand);
        Task<IEnumerable<Vehicle>> GetVehiclesByYearAsync(int year);
    }
}
