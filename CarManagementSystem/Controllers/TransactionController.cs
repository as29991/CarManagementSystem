using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarManagementSystem.DTOs;
using CarManagementSystem.Services.Interfaces;

namespace CarManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDTO>> AddTransaction(CreateTransactionDTO createTransactionDTO)
        {
            await _transactionService.AddTransactionAsync(createTransactionDTO);
            return CreatedAtAction(nameof(GetTransactionById), new { id = createTransactionDTO.VehicleId }, createTransactionDTO);
        }
    }
}