using CarManagementSystem.Models;

namespace CarManagementSystem.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task AddTransactionAsync(Transaction transaction);

    }
}
