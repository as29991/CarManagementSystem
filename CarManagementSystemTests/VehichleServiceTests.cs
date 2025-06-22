using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using CarManagementSystem.DTOs;
using CarManagementSystem.Models;
using CarManagementSystem.Repositories.Interfaces;
using CarManagementSystem.Services.Implementations;

namespace CarManagementSystem.Tests
{
    public class VehicleServiceTests
    {
        private readonly Mock<IVehicleRepository> _mockVehicleRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly VehicleService _service;

        public VehicleServiceTests()
        {
            _mockVehicleRepository = new Mock<IVehicleRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new VehicleService(_mockVehicleRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task AddVehicleAsync_AddsVehicle()
        {
            // Arrange
            var createVehicleDTO = new CreateVehicleDTO { Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000 };
            var vehicle = new Vehicle { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Available };
            _mockMapper.Setup(m => m.Map<Vehicle>(createVehicleDTO)).Returns(vehicle);
            _mockVehicleRepository.Setup(repo => repo.AddVehicleAsync(vehicle)).Returns(Task.CompletedTask);

            // Act
            await _service.AddVehicleAsync(createVehicleDTO);

            // Assert
            _mockVehicleRepository.Verify(repo => repo.AddVehicleAsync(It.IsAny<Vehicle>()), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleAsync_DeletesVehicle()
        {
            // Arrange
            int vehicleId = 1;
            _mockVehicleRepository.Setup(repo => repo.DeleteVehicleAsync(vehicleId)).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteVehicleAsync(vehicleId);

            // Assert
            _mockVehicleRepository.Verify(repo => repo.DeleteVehicleAsync(vehicleId), Times.Once);
        }

       

        [Fact]
        public async Task GetVehiclesByBrandAsync_ReturnsVehicleDTOs()
        {
            // Arrange
            var vehicles = new List<Vehicle>
            {
                new Vehicle { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Available }
            };
            var vehicleDTOs = new List<VehicleDTO>
            {
                new VehicleDTO { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Available }
            };
            _mockVehicleRepository.Setup(repo => repo.GetVehiclesByBrandAsync("Toyota")).ReturnsAsync(vehicles);
            _mockMapper.Setup(m => m.Map<IEnumerable<VehicleDTO>>(vehicles)).Returns(vehicleDTOs);

            // Act
            var result = await _service.GetVehiclesByBrandAsync("Toyota");

            // Assert
            Assert.Equal(vehicleDTOs, result.ToList());
        }

        [Fact]
        public async Task GetVehiclesByModelAsync_ReturnsVehicleDTOs()
        {
            // Arrange
            var vehicles = new List<Vehicle>
            {
                new Vehicle { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Available }
            };
            var vehicleDTOs = new List<VehicleDTO>
            {
                new VehicleDTO { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Available }
            };
            _mockVehicleRepository.Setup(repo => repo.GetVehiclesByModelAsync("Corolla")).ReturnsAsync(vehicles);
            _mockMapper.Setup(m => m.Map<IEnumerable<VehicleDTO>>(vehicles)).Returns(vehicleDTOs);

            // Act
            var result = await _service.GetVehiclesByModelAsync("Corolla");

            // Assert
            Assert.Equal(vehicleDTOs, result.ToList());
        }

        [Fact]
        public async Task GetVehiclesByYearAsync_ReturnsVehicleDTOs()
        {
            // Arrange
            var vehicles = new List<Vehicle>
            {
                new Vehicle { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Available }
            };
            var vehicleDTOs = new List<VehicleDTO>
            {
                new VehicleDTO { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Available }
            };
            _mockVehicleRepository.Setup(repo => repo.GetVehiclesByYearAsync(2020)).ReturnsAsync(vehicles);
            _mockMapper.Setup(m => m.Map<IEnumerable<VehicleDTO>>(vehicles)).Returns(vehicleDTOs);

            // Act
            var result = await _service.GetVehiclesByYearAsync(2020);

            // Assert
            Assert.Equal(vehicleDTOs, result.ToList());
        }

        [Fact]
        public async Task UpdateVehicleAsync_UpdatesVehicle()
        {
            // Arrange
            var vehicle = new Vehicle { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Available };
            _mockVehicleRepository.Setup(repo => repo.UpdateVehicleAsync(vehicle, vehicle.Id)).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateVehicleAsync(vehicle, vehicle.Id);

            // Assert
            _mockVehicleRepository.Verify(repo => repo.UpdateVehicleAsync(vehicle, vehicle.Id), Times.Once);
        }
    }
}
