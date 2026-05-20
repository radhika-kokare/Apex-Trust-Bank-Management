using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApexTrustBank.BLL.Interfaces;
using ApexTrustBank.Models.Entities;
using ApexTrustBank.Models.DTOs;
using ApexTrustBank.BLL.DTOs;


namespace ApexTrustBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Get all accounts
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAsync();
            return Ok(accounts);
        }

        /// <summary>
        /// Get account by id
        /// </summary>
        [HttpGet("{AccountId:int}")]
        public async Task<IActionResult> GetAccountById(int AccountId)
        {
            var account = await _accountService.GetAccountByIdAsync(AccountId);

            if (account == null)
                return NotFound(new { message = "Account not found" });

            return Ok(account);
        }

        /// <summary>
        /// Create new bank account
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountId = await _accountService.CreateAccountAsync(dto);

            return CreatedAtAction(
                nameof(GetAccountById),
                new { accountId },
            new { message = "Account created successfully", accountId }
            );
        }






        /// <summary>
        /// Update account active / inactive status
        /// </summary>
        [HttpPut("{accountId:int}/status")]
        public async Task<IActionResult> UpdateAccountStatus(int accountId, [FromBody] bool isActive)
        {
            if (accountId <= 0)
                return BadRequest("Invalid Account Id");

            var result = await _accountService.UpdateAccountStatusAsync(accountId,isActive);

            if (!result)
                return NotFound(new { message = "Account not found" });

            return Ok(new { message = "Account status updated successfully" });
        }

        /// <summary>
        /// Get account balance by account number
        /// </summary>
        [HttpGet("Balance/{accountNumber}")]
        public async Task<IActionResult> GetBalanceByAccountNumber(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                return BadRequest("Account number is required");

            var balance = await _accountService.GetBalanceByAccountNumberAsync(accountNumber);

            if (balance == null)
                return NotFound(new { message = "Account not found" });

            return Ok(new
            {
                AccountNumber = accountNumber,
                Balance = balance
            });
        }

        /// <summary>
        /// Get customer list
        /// </summary>
        [HttpGet("CustomerList")]
        public async Task<IActionResult> GetCustomerList()
        {
            var customers = await _accountService.GetCustomerListAsync();

            if (!customers.Any())
                return Ok(new List<CustomerDTO>());

            return Ok(customers);
        }


    }
}
    