using AutoMapper;
using CarManagementSystem.DTOs;
using CarManagementSystem.Models;
using CarManagementSystem.Repositories.Interfaces;
using CarManagementSystem.Services.Interfaces;
using System.Xml.Linq;

namespace CarManagementSystem.Services.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task AddVehicleAsync(CreateVehicleDTO createVehicleDTO)
        {
            var vehicle = _mapper.Map<Vehicle>(createVehicleDTO);
            vehicle.Status = VehicleStatus.Available;
            await _vehicleRepository.AddVehicleAsync(vehicle);
        }

        public async Task DeleteVehicleAsync(int id)
        {
            await _vehicleRepository.DeleteVehicleAsync(id);
        }

        public async Task<IEnumerable<VehicleDTO>> GetAllVehiclesAsync()
        {
            var vehicles = await _vehicleRepository.GetAllVehiclesAsync();

            var response = vehicles?.Select(element =>
            {
                VehicleDTO vehicleDto = new VehicleDTO();

                return _mapper.Map(element, vehicleDto);
            });

            return response;
        }

        public async Task<VehicleDTO> GetVehicleByIdAsync(int id)
        {
            var vehicles = await _vehicleRepository.GetVehicleByIdAsync(id);

            VehicleDTO vehicleDto = new VehicleDTO();

            var response = _mapper.Map(vehicles, vehicleDto);

            return response;
        }

        public async Task<IEnumerable<VehicleDTO>> GetVehiclesByBrandAsync(string brand)
        {
            var vehicles = await _vehicleRepository.GetVehiclesByBrandAsync(brand);
            return _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);
        }

        public async Task<IEnumerable<VehicleDTO>> GetVehiclesByModelAsync(string model)
        {
            var vehicles = await _vehicleRepository.GetVehiclesByModelAsync(model);
            return _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);
        }

    
        public async Task<IEnumerable<VehicleDTO>> GetVehiclesByYearAsync(int year)
        {
            var vehicles = await _vehicleRepository.GetVehiclesByYearAsync(year);
            return _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle, int id)
        {
            await _vehicleRepository.UpdateVehicleAsync(vehicle, id);
        }


    }
}
