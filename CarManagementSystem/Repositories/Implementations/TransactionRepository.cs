using CarManagementSystem.Data;
using CarManagementSystem.Models;
using CarManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarManagementSystem.Repositories.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.Include(t => t.Vehicle).Include(t => t.User).ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions.Include(t => t.Vehicle).Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

    }
}
