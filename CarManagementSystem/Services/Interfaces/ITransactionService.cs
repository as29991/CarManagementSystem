using System.Collections.Generic;
using System.Threading.Tasks;
using CarManagementSystem.DTOs;

namespace CarManagementSystem.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync();
        Task<TransactionDTO> GetTransactionByIdAsync(int id);
        Task<TransactionDTO> AddTransactionAsync(CreateTransactionDTO createTransactionDTO);
    }
}