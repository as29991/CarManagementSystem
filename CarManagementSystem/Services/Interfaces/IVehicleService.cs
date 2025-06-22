using System;
using CarManagementSystem.DTOs;
using CarManagementSystem.Models;

namespace CarManagementSystem.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDTO>> GetAllVehiclesAsync();
        
        Task<VehicleDTO> GetVehicleByIdAsync(int id);
        
        Task AddVehicleAsync(CreateVehicleDTO createVehicleDto);
        
        Task UpdateVehicleAsync(Vehicle vehicle, int id);

        Task DeleteVehicleAsync(int id);
        
        Task<IEnumerable<VehicleDTO>> GetVehiclesByModelAsync(string model);
        
        Task<IEnumerable<VehicleDTO>> GetVehiclesByBrandAsync(string brand);
        
        Task<IEnumerable<VehicleDTO>> GetVehiclesByYearAsync(int year);


    }
}
