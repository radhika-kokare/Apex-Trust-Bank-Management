//using ApexTrustBank.BLL.DTOs;
using ApexTrustBank.BLL.DTOs;
using ApexTrustBank.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexTrustBank.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAllAsync();
        Task<AccountDTO?> GetAccountByIdAsync(int AccountId);
        Task<int> CreateAccountAsync(AccountDTO dto);
        Task<bool> UpdateAccountStatusAsync(int accountId, bool isActive);
        Task<decimal?> GetBalanceByAccountNumberAsync(string accountNumber);
        Task<IEnumerable<CustomerDTO>> GetCustomerListAsync();
    }
}