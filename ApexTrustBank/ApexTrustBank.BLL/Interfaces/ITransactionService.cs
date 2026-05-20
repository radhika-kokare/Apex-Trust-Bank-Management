using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexTrustBank.Models.Entities;

namespace ApexTrustBank.BLL.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> DepositAsync(string accountNumber, decimal amount);
        Task<bool> WithdrawAsync(string accountNumber, decimal amount);
        Task<bool> TransferAsync(string fromAccount, string toAccount, decimal amount);
        Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(string accountNumber);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();


    }
}


