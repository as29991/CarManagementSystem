using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarManagementSystem.DTOs;
using CarManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CarManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetAllTransactions()
        {
            try
            {
                _logger.LogInformation("Fetching all transactions");
                var transactions = await _transactionService.GetAllTransactionsAsync();
                _logger.LogInformation($"Retrieved {transactions.Count()} transactions");
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all transactions");
                return StatusCode(500, "An error occurred while fetching transactions");
            }
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
            try
            {
                _logger.LogInformation($"Creating transaction for VehicleId: {createTransactionDTO.VehicleId}, UserId: {createTransactionDTO.UserId}, Amount: {createTransactionDTO.Amount}");

                if (string.IsNullOrEmpty(createTransactionDTO.UserId))
                {
                    _logger.LogWarning("UserId is null or empty");
                    return BadRequest("UserId is required");
                }

                var result = await _transactionService.AddTransactionAsync(createTransactionDTO);
                _logger.LogInformation($"Transaction created successfully for UserId: {createTransactionDTO.UserId}");

                return CreatedAtAction(nameof(GetTransactionById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating transaction for VehicleId: {createTransactionDTO.VehicleId}, UserId: {createTransactionDTO.UserId}");
                return StatusCode(500, $"Transaction creation failed: {ex.Message}");
            }
        }
    }
}