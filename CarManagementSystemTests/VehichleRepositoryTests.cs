using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using CarManagementSystem.Data;
using CarManagementSystem.DTOs;
using CarManagementSystem.Models;
using CarManagementSystem.Repositories.Implementations;

namespace CarManagementSystem.Tests
{
    public class VehicleRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public VehicleRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        private async Task<ApplicationDbContext> CreateContextWithData()
        {
            var context = new ApplicationDbContext(_dbContextOptions);

            context.Vehicles.AddRange(
                new Vehicle { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Available },
                new Vehicle { Id = 2, Brand = "Honda", Model = "Civic", Year = 2021, Price = 22000, Status = VehicleStatus.Available }
            );

            await context.SaveChangesAsync();

            return context;
        }

        [Fact]
        public async Task AddVehicleAsync_AddsVehicleToDatabase()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new VehicleRepository(context);
            var vehicle = new Vehicle { Brand = "Ford", Model = "Focus", Year = 2019, Price = 18000 };

            await repository.AddVehicleAsync(vehicle);
            var vehiclesInDb = await context.Vehicles.ToListAsync();

            Assert.Single(vehiclesInDb);
            Assert.Equal("Ford", vehiclesInDb[0].Brand);
        }

        [Fact]
        public async Task DeleteVehicleAsync_RemovesVehicleFromDatabase()
        {
            using var context = await CreateContextWithData();
            var repository = new VehicleRepository(context);
            int vehicleId = 1;

            await repository.DeleteVehicleAsync(vehicleId);
            var vehiclesInDb = await context.Vehicles.ToListAsync();

            Assert.Single(vehiclesInDb);
            Assert.Equal(2, vehiclesInDb[0].Id); // Ensure the remaining vehicle is the second one
        }

        [Fact]
        public async Task GetAllVehiclesAsync_ReturnsAllVehicles()
        {
            using var context = await CreateContextWithData();
            var repository = new VehicleRepository(context);

            var vehicles = await repository.GetAllVehiclesAsync();

            Assert.Equal(2, vehicles.Count());
        }

        [Fact]
        public async Task GetVehicleByIdAsync_ReturnsCorrectVehicle()
        {
            using var context = await CreateContextWithData();
            var repository = new VehicleRepository(context);
            int vehicleId = 1;

            var vehicle = await repository.GetVehicleByIdAsync(vehicleId);

            Assert.NotNull(vehicle);
            Assert.Equal("Toyota", vehicle.Brand);
        }

        [Fact]
        public async Task GetVehiclesByBrandAsync_ReturnsVehiclesOfSpecificBrand()
        {
            using var context = await CreateContextWithData();
            var repository = new VehicleRepository(context);
            string brand = "Honda";

            var vehicles = await repository.GetVehiclesByBrandAsync(brand);

            Assert.Single(vehicles);
            Assert.Equal("Honda", vehicles.First().Brand);
        }

        [Fact]
        public async Task GetVehiclesByModelAsync_ReturnsVehiclesOfSpecificModel()
        {
            using var context = await CreateContextWithData();
            var repository = new VehicleRepository(context);
            string model = "Civic";

            var vehicles = await repository.GetVehiclesByModelAsync(model);

            Assert.Single(vehicles);
            Assert.Equal("Civic", vehicles.First().Model);
        }

        [Fact]
        public async Task GetVehiclesByYearAsync_ReturnsVehiclesOfSpecificYear()
        {
            using var context = await CreateContextWithData();
            var repository = new VehicleRepository(context);
            int year = 2020;

            var vehicles = await repository.GetVehiclesByYearAsync(year);

            Assert.Single(vehicles);
            Assert.Equal(2020, vehicles.First().Year);
        }

        [Fact]
        public async Task UpdateVehicleAsync_UpdatesVehicleInDatabase()
        {
            using var context = await CreateContextWithData();
            var repository = new VehicleRepository(context);
            var updatedVehicle = new Vehicle { Brand = "Toyota", Model = "Camry", Year = 2022, Price = 25000 };
            int vehicleId = 1;

            await repository.UpdateVehicleAsync(updatedVehicle, vehicleId);
            var vehicleInDb = await context.Vehicles.FindAsync(vehicleId);

            Assert.NotNull(vehicleInDb);
            Assert.Equal("Camry", vehicleInDb.Model);
            Assert.Equal(2022, vehicleInDb.Year);
        }
    }
}
