using ApexTrustBank.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApexTrustBank.Models.Entities;
using ApexTrustBank.Models.DTOs;
using ApexTrustBank.BLL.DTOs;

namespace ApexTrustBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Deposit amount into account
        /// </summary>
        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositDTO request)
        {
            var result = await _transactionService.DepositAsync(request.AccountNumber,request.Amount);

            if (!result)
                return NotFound(new { message = "Account not found" });

            return Ok(new { message = "Deposit successful" });
        }

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawDTO request)
        {
           
            var result = await _transactionService.WithdrawAsync(
                request.AccountNumber,
                request.Amount
            );

            if (!result)
                return BadRequest(new { message = "Insufficient balance or account not found" });

            return Ok(new { message = "Withdrawal successful" });
        }


        // POST: api/Transactions/Transfer
        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDTO request)
        {   
            var result = await _transactionService.TransferAsync(
                request.FromAccountNumber,
                request.ToAccountNumber,
                request.Amount
            );

            if (!result)
                return BadRequest(new
                {
                    message = "Transfer failed. Check account details or balance."
                });

            return Ok(new { message = "Transfer successful" });
        }

        [HttpGet("History/{accountNumber}")]
        public async Task<IActionResult> GetHistory(string accountNumber)
        {
            var history = await _transactionService.GetTransactionHistoryAsync(accountNumber);

            if (!history.Any())
                return NotFound(new { message = "No transactions found" });

            return Ok(history);
        }

        /// <summary>
        /// Get all transactions
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();

            if (!transactions.Any())
                return NotFound(new { message = "No transactions found" });

            return Ok(transactions);
        }
    }
}

