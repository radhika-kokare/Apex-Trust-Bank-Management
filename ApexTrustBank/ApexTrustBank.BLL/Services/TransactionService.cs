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
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> DepositAsync(string accountNumber, decimal amount)
        {
            return await _transactionRepository.DepositAsync(accountNumber, amount);
        }

        public async Task<bool> WithdrawAsync(string accountNumber, decimal amount)
        {
            return await _transactionRepository.WithdrawAsync(accountNumber, amount);
        }

        public async Task<bool> TransferAsync(string fromAccount, string toAccount, decimal amount)
        {
            return await _transactionRepository.TransferAsync(fromAccount, toAccount, amount);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(string accountNumber)
        {
            return await _transactionRepository.GetTransactionHistoryAsync(accountNumber);
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }
    }

}
