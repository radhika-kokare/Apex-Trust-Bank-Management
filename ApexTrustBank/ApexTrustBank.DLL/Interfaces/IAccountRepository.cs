using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexTrustBank.Models.Entities;

namespace ApexTrustBank.DLL.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetAccountByIdAsync(int AccountId);
        Task<int> CreateAccountAsync(Account account);
        Task<bool> UpdateAccountStatusAsync(int accountId, bool isActive);
        Task<decimal?> GetBalanceByAccountNumberAsync(string accountNumber);

        Task<IEnumerable<Account>> GetCustomerListAsync();
    }
}
