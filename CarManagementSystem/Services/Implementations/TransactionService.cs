using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarManagementSystem.DTOs;
using CarManagementSystem.Models;
using CarManagementSystem.Repositories.Interfaces;
using CarManagementSystem.Services.Interfaces;

namespace CarManagementSystem.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
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

        public async Task AddTransactionAsync(CreateTransactionDTO createTransactionDTO)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetVehicleByIdAsync(createTransactionDTO.VehicleId);
                if (vehicle == null || vehicle.Status == VehicleStatus.Sold)
                {
                    throw new Exception("Vehicle not available");
                }

                var transaction = new Transaction
                {
                    VehicleId = createTransactionDTO.VehicleId,
                    UserId = createTransactionDTO.UserId,
                    Amount = createTransactionDTO.Amount,
                    TransactionDate = DateTime.UtcNow
                };

                await _transactionRepository.AddTransactionAsync(transaction);

                // Update the status of the vehicle to Sold
                vehicle.Status = VehicleStatus.Sold;
                await _vehicleRepository.UpdateVehicleAsync(vehicle, vehicle.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddTransactionAsync: {ex.Message}");
                throw; 
            }
        }
    }
}
