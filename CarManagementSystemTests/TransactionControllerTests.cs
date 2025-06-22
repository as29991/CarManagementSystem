using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using CarManagementSystem.Controllers;
using CarManagementSystem.DTOs;
using CarManagementSystem.Services.Interfaces;

namespace CarManagementSystem.Tests
{
    public class TransactionControllerTests
    {
        private readonly Mock<ITransactionService> _mockTransactionService;
        private readonly TransactionController _controller;

        public TransactionControllerTests()
        {
            _mockTransactionService = new Mock<ITransactionService>();
            _controller = new TransactionController(_mockTransactionService.Object);
        }

        [Fact]
        public async Task GetAllTransactions_ReturnsOkResult_WithListOfTransactions()
        {
            // Arrange
            var transactions = new List<TransactionDTO>
            {
                new TransactionDTO { VehicleId = 1, Amount = 1000 },
                new TransactionDTO { VehicleId = 2, Amount = 2000 }
            };
            _mockTransactionService.Setup(service => service.GetAllTransactionsAsync()).ReturnsAsync(transactions);

            // Act
            var result = await _controller.GetAllTransactions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<TransactionDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetTransactionById_ReturnsOkResult_WithTransaction()
        {
            // Arrange
            var transaction = new TransactionDTO { VehicleId = 1, Amount = 1000 };
            _mockTransactionService.Setup(service => service.GetTransactionByIdAsync(1)).ReturnsAsync(transaction);

            // Act
            var result = await _controller.GetTransactionById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TransactionDTO>(okResult.Value);
            Assert.Equal(1, returnValue.VehicleId);
        }

        [Fact]
        public async Task GetTransactionById_ReturnsNotFoundResult_WhenTransactionNotFound()
        {
            // Arrange
            _mockTransactionService.Setup(service => service.GetTransactionByIdAsync(1)).ReturnsAsync((TransactionDTO)null);

            // Act
            var result = await _controller.GetTransactionById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddTransaction_ReturnsCreatedAtActionResult_WithTransaction()
        {
            // Arrange
            var createTransactionDTO = new CreateTransactionDTO { VehicleId = 1, Amount = 1000 };
            _mockTransactionService.Setup(service => service.AddTransactionAsync(createTransactionDTO)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddTransaction(createTransactionDTO);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<CreateTransactionDTO>(createdAtActionResult.Value);
            Assert.Equal(createTransactionDTO.VehicleId, returnValue.VehicleId);
        }
    }
}
