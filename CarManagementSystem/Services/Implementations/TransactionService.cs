using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarManagementSystem.DTOs;
using CarManagementSystem.Models;
using CarManagementSystem.Repositories.Interfaces;
using CarManagementSystem.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarManagementSystem.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IVehicleRepository vehicleRepository,
            IMapper mapper,
            ILogger<TransactionService> logger)
        {
            _transactionRepository = transactionRepository;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            return transactions.Select(t => new TransactionDTO
            {
                Id = t.Id,
                VehicleId = t.VehicleId,
                UserId = t.UserId,
                TransactionDate = t.TransactionDate,
                Amount = t.Amount
            });
        }

        public async Task<TransactionDTO> GetTransactionByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            return _mapper.Map<TransactionDTO>(transaction);
        }

        public async Task<TransactionDTO> AddTransactionAsync(CreateTransactionDTO createTransactionDTO)
        {
            using var transaction = await _transactionRepository.BeginTransactionAsync();
            try
            {
                _logger.LogInformation($"Starting transaction creation for VehicleId: {createTransactionDTO.VehicleId}, UserId: {createTransactionDTO.UserId}");

                // Validate inputs
                if (string.IsNullOrEmpty(createTransactionDTO.UserId))
                {
                    throw new ArgumentException("UserId cannot be null or empty");
                }

                if (createTransactionDTO.VehicleId <= 0)
                {
                    throw new ArgumentException("VehicleId must be greater than 0");
                }

                if (createTransactionDTO.Amount <= 0)
                {
                    throw new ArgumentException("Amount must be greater than 0");
                }

                var vehicle = await _vehicleRepository.GetVehicleByIdAsync(createTransactionDTO.VehicleId);
                if (vehicle == null)
                {
                    _logger.LogWarning($"Vehicle with ID {createTransactionDTO.VehicleId} not found");
                    throw new Exception("Vehicle not found");
                }

                if (vehicle.Status == VehicleStatus.Sold)
                {
                    _logger.LogWarning($"Vehicle with ID {createTransactionDTO.VehicleId} is already sold");
                    throw new Exception("Vehicle is already sold");
                }

                var newTransaction = new Transaction
                {
                    VehicleId = createTransactionDTO.VehicleId,
                    UserId = createTransactionDTO.UserId,
                    Amount = createTransactionDTO.Amount,
                    TransactionDate = DateTime.UtcNow
                };

                _logger.LogInformation($"Adding transaction to database: VehicleId={newTransaction.VehicleId}, UserId={newTransaction.UserId}, Amount={newTransaction.Amount}");
                await _transactionRepository.AddTransactionAsync(newTransaction);

                // Update the status of the vehicle to Sold
                vehicle.Status = VehicleStatus.Sold;
                _logger.LogInformation($"Updating vehicle {vehicle.Id} status to Sold");
                await _vehicleRepository.UpdateVehicleAsync(vehicle, vehicle.Id);

                await transaction.CommitAsync();
                _logger.LogInformation($"Transaction completed successfully with ID: {newTransaction.Id}");

                return new TransactionDTO
                {
                    Id = newTransaction.Id,
                    VehicleId = newTransaction.VehicleId,
                    UserId = newTransaction.UserId,
                    TransactionDate = newTransaction.TransactionDate,
                    Amount = newTransaction.Amount
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Error in AddTransactionAsync for VehicleId: {createTransactionDTO.VehicleId}, UserId: {createTransactionDTO.UserId}");
                throw;
            }
        }
    }
}