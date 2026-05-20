using ApexTrustBank.Models.DTOs;
using ApexTrustBank.BLL.Interfaces;
using ApexTrustBank.DLL.Interfaces;
using ApexTrustBank.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexTrustBank.BLL.DTOs;

namespace ApexTrustBank.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();

            return accounts.Select(a => new AccountDTO
            {
                AccountId = a.AccountId,
                AccountType = a.AccountType,
                AccountNumber = a.AccountNumber,
                Balance = a.Balance,
                IsActive = a.IsActive,
                CreatedDate = a.CreatedDate,
                FullName = a.FullName,
                Email = a.Email,
                Mobile = a.Mobile,
                Address = a.Address
            });
        }

        public async Task<AccountDTO?> GetAccountByIdAsync(int AccountId)
        {
            var account = await _accountRepository.GetAccountByIdAsync(AccountId);

            if (account == null)
                return null;

            return new AccountDTO
            {
                AccountId = account.AccountId,
                AccountType = account.AccountType,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                IsActive = account.IsActive,
                CreatedDate = account.CreatedDate,
                FullName = account.FullName,
                Email = account.Email,
                Mobile = account.Mobile,
                Address = account.Address
            };
        }

        public async Task<int> CreateAccountAsync(AccountDTO dto)
        {
            var account = new Account
            {
                AccountType = dto.AccountType,
                AccountNumber = dto.AccountNumber,
                Balance = dto.Balance,
                FullName = dto.FullName,
                Email = dto.Email,
                Mobile = dto.Mobile,
                Address = dto.Address
            };

            return await _accountRepository.CreateAccountAsync(account);
        }
        
        public async Task<bool> UpdateAccountStatusAsync(int accountId, bool isActive)
        {
            return await _accountRepository.UpdateAccountStatusAsync(accountId, isActive);
        }

        public async Task<decimal?> GetBalanceByAccountNumberAsync(string accountNumber)
        {
            return await _accountRepository.GetBalanceByAccountNumberAsync(accountNumber);
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomerListAsync()
        {
            var accounts = await _accountRepository.GetCustomerListAsync();

            return accounts.Select(a => new CustomerDTO
            {
                AccountId = a.AccountId,
                FullName = a.FullName,
                Email = a.Email,
                Mobile = a.Mobile,
                AccountNumber = a.AccountNumber,
                IsActive = a.IsActive
            });
        }

    }
}
