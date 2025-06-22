using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using CarManagementSystem.DTOs;
using CarManagementSystem.Models;
using CarManagementSystem.Repositories.Interfaces;
using CarManagementSystem.Services;
using CarManagementSystem.Services.Interfaces;

namespace CarManagementSystem.Tests
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;
        private readonly Mock<IVehicleRepository> _mockVehicleRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TransactionService _service;

        public TransactionServiceTests()
        {
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _mockVehicleRepository = new Mock<IVehicleRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new TransactionService(_mockTransactionRepository.Object, _mockVehicleRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllTransactionsAsync_ReturnsTransactionDTOs()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, VehicleId = 1, UserId = "1", Amount = 20000, TransactionDate = DateTime.UtcNow },
                new Transaction { Id = 2, VehicleId = 2, UserId = "2", Amount = 25000, TransactionDate = DateTime.UtcNow }
            };
            var transactionDTOs = new List<TransactionDTO>
            {
                new TransactionDTO { Id = 1, VehicleId = 1, UserId = "1", Amount = 20000, TransactionDate = DateTime.UtcNow },
                new TransactionDTO { Id = 2, VehicleId = 2, UserId = "2", Amount = 25000, TransactionDate = DateTime.UtcNow }
            };
            _mockTransactionRepository.Setup(repo => repo.GetAllTransactionsAsync()).ReturnsAsync(transactions);
            _mockMapper.Setup(m => m.Map<IEnumerable<TransactionDTO>>(transactions)).Returns(transactionDTOs);

            // Act
            var result = await _service.GetAllTransactionsAsync();

            // Assert
            Assert.Equal(transactionDTOs, result);
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ReturnsTransactionDTO()
        {
            // Arrange
            var transaction = new Transaction { Id = 1, VehicleId = 1, UserId = "1", Amount = 20000, TransactionDate = DateTime.UtcNow };
            var transactionDTO = new TransactionDTO { Id = 1, VehicleId = 1, UserId = "1", Amount = 20000, TransactionDate = DateTime.UtcNow };
            _mockTransactionRepository.Setup(repo => repo.GetTransactionByIdAsync(1)).ReturnsAsync(transaction);
            _mockMapper.Setup(m => m.Map<TransactionDTO>(transaction)).Returns(transactionDTO);

            // Act
            var result = await _service.GetTransactionByIdAsync(1);

            // Assert
            Assert.Equal(transactionDTO, result);
        }

        [Fact]
        public async Task AddTransactionAsync_AddsTransaction()
        {
            // Arrange
            var createTransactionDTO = new CreateTransactionDTO { VehicleId = 1, UserId = "1", Amount = 20000 };
            var vehicle = new Vehicle { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Available };
            _mockVehicleRepository.Setup(repo => repo.GetVehicleByIdAsync(createTransactionDTO.VehicleId)).ReturnsAsync(vehicle);
            _mockTransactionRepository.Setup(repo => repo.AddTransactionAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);
            _mockVehicleRepository.Setup(repo => repo.UpdateVehicleAsync(vehicle, vehicle.Id)).Returns(Task.CompletedTask);

            // Act
            await _service.AddTransactionAsync(createTransactionDTO);

            // Assert
            _mockTransactionRepository.Verify(repo => repo.AddTransactionAsync(It.IsAny<Transaction>()), Times.Once);
            _mockVehicleRepository.Verify(repo => repo.UpdateVehicleAsync(vehicle, vehicle.Id), Times.Once);
            Assert.Equal(VehicleStatus.Sold, vehicle.Status);
        }

        [Fact]
        public async Task AddTransactionAsync_ThrowsException_WhenVehicleNotAvailable()
        {
            // Arrange
            var createTransactionDTO = new CreateTransactionDTO { VehicleId = 1, UserId = "1", Amount = 20000 };
            var vehicle = new Vehicle { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Sold };
            _mockVehicleRepository.Setup(repo => repo.GetVehicleByIdAsync(createTransactionDTO.VehicleId)).ReturnsAsync(vehicle);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AddTransactionAsync(createTransactionDTO));
        }
    }
}
