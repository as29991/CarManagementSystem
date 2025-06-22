using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using CarManagementSystem.Data;
using CarManagementSystem.Models;
using CarManagementSystem.Repositories.Implementations;

namespace CarManagementSystem.Tests
{
    public class TransactionRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(dbContextOptions);
            SeedData(_context).Wait();
        }

        private async Task SeedData(ApplicationDbContext context)
        {
            var vehicle = new Vehicle { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Status = VehicleStatus.Available };
            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            var transaction = new Transaction { Id = 1, UserId = "1", VehicleId = vehicle.Id, Amount = 20000, TransactionDate = DateTime.Now, Vehicle = vehicle };
            context.Transactions.Add(transaction);

            await context.SaveChangesAsync();
        }



        [Fact]
        public async Task AddTransactionAsync_AddsTransactionToDatabase()
        {
            var repository = new TransactionRepository(_context);
            var vehicle = new Vehicle { Id = 2, Brand = "Honda", Model = "Civic", Year = 2021, Price = 25000, Status = VehicleStatus.Available };
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            var transaction = new Transaction
            {
                UserId = "2",
                VehicleId = vehicle.Id,
                Amount = 25000,
                TransactionDate = DateTime.Now,
                Vehicle = vehicle
            };

            await repository.AddTransactionAsync(transaction);
            var transactionsInDb = await _context.Transactions.Include(t => t.Vehicle).ToListAsync();

            Assert.Equal(2, transactionsInDb.Count);
            Assert.Equal(25000, transactionsInDb[1].Amount);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
