using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using CarManagementSystem.Controllers;
using CarManagementSystem.DTOs;
using CarManagementSystem.Models;
using CarManagementSystem.Services.Interfaces;

namespace CarManagementSystem.Tests
{
    public class VehicleControllerTests
    {
        private readonly Mock<IVehicleService> _mockVehicleService;
        private readonly VehicleController _controller;

        public VehicleControllerTests()
        {
            _mockVehicleService = new Mock<IVehicleService>();
            _controller = new VehicleController(_mockVehicleService.Object);
        }

        [Fact]
        public async Task GetAllVehiclesAsync_ReturnsOkResult_WithListOfVehicles()
        {
            // Arrange
            var vehicles = new List<VehicleDTO>
            {
                new VehicleDTO { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 },
                new VehicleDTO { Id = 2, Brand = "Honda", Model = "Civic", Year = 2021 }
            };
            _mockVehicleService.Setup(service => service.GetAllVehiclesAsync()).Returns(Task.FromResult<IEnumerable<VehicleDTO>>(vehicles));

            // Act
            var result = await _controller.GetAllVehiclesAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<VehicleDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task AddVehicleAsync_ReturnsOkResult()
        {
            // Arrange
            var createVehicleDto = new CreateVehicleDTO { Brand = "Toyota", Model = "Corolla", Year = 2020 };
            _mockVehicleService.Setup(service => service.AddVehicleAsync(createVehicleDto)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddVehicleAsync(createVehicleDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteVehicleAsync_ReturnsOkResult()
        {
            // Arrange
            int vehicleId = 1;
            _mockVehicleService.Setup(service => service.DeleteVehicleAsync(vehicleId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteVehicleAsync(vehicleId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetVehicleByIdAsync_ReturnsOkResult_WithVehicle()
        {
            // Arrange
            var vehicle = new VehicleDTO { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 };
            _mockVehicleService.Setup(service => service.GetVehicleByIdAsync(1)).Returns(Task.FromResult(vehicle));

            // Act
            var result = await _controller.GetVehicleByIdAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<VehicleDTO>(okResult.Value);
            Assert.Equal("Toyota", returnValue.Brand);
        }

        [Fact]
        public async Task UpdateVehicleAsync_ReturnsOkResult()
        {
            // Arrange
            var vehicle = new Vehicle { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 };
            _mockVehicleService.Setup(service => service.UpdateVehicleAsync(vehicle, 1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateVehicleAsync(vehicle, 1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetVehiclesByBrandAsync_ReturnsOkResult_WithVehicles()
        {
            // Arrange
            var vehicles = new List<VehicleDTO>
            {
                new VehicleDTO { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 },
                new VehicleDTO { Id = 2, Brand = "Toyota", Model = "Camry", Year = 2021 }
            };
            _mockVehicleService.Setup(service => service.GetVehiclesByBrandAsync("Toyota")).Returns(Task.FromResult<IEnumerable<VehicleDTO>>(vehicles));

            // Act
            var result = await _controller.GetVehiclesByBrandAsync("Toyota");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<VehicleDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetVehiclesByModelAsync_ReturnsOkResult_WithVehicles()
        {
            // Arrange
            var vehicles = new List<VehicleDTO>
            {
                new VehicleDTO { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 },
                new VehicleDTO { Id = 2, Brand = "Honda", Model = "Civic", Year = 2021 }
            };
            _mockVehicleService.Setup(service => service.GetVehiclesByModelAsync("Civic")).Returns(Task.FromResult<IEnumerable<VehicleDTO>>(new List<VehicleDTO> { vehicles[1] }));

            // Act
            var result = await _controller.GetVehiclesByModelAsync("Civic");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<VehicleDTO>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Civic", returnValue[0].Model);
        }

        [Fact]
        public async Task GetVehiclesByYearAsync_ReturnsOkResult_WithVehicles()
        {
            // Arrange
            var vehicles = new List<VehicleDTO>
            {
                new VehicleDTO { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 },
                new VehicleDTO { Id = 2, Brand = "Honda", Model = "Civic", Year = 2020 }
            };
            _mockVehicleService.Setup(service => service.GetVehiclesByYearAsync(2020)).Returns(Task.FromResult<IEnumerable<VehicleDTO>>(vehicles));

            // Act
            var result = await _controller.GetVehiclesByYearAsync(2020);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<VehicleDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
    }
}
