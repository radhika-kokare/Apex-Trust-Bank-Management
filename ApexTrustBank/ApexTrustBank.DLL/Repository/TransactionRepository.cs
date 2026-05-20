using ApexTrustBank.DLL.Infrastructure;
using ApexTrustBank.DLL.Interfaces;
using ApexTrustBank.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;


namespace ApexTrustBank.DLL.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbConnectionFactory _db;

        public TransactionRepository(IDbConnectionFactory db)
        {
            _db = db;
        }
        public async Task<bool> DepositAsync(string accountNumber, decimal amount)
        {
            using var conn = _db.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@AccountNumber", accountNumber.Trim());
            parameters.Add("@Amount", amount);
            parameters.Add("@Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await conn.ExecuteAsync(
                "sp_Transactions_Deposit",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return parameters.Get<bool>("@Result");
        }

        public async Task<bool> WithdrawAsync(string accountNumber, decimal amount)
        {
            using var conn = _db.CreateConnection();

            var result = await conn.ExecuteScalarAsync<int>(
                "sp_Transactions_Withdraw",
                new { AccountNumber = accountNumber, Amount = amount },
                commandType: CommandType.StoredProcedure
            );

            return result == 1;
        }

        public async Task<bool> TransferAsync(string fromAccount, string toAccount, decimal amount)
        {
            using var conn = _db.CreateConnection();

            var result = await conn.ExecuteScalarAsync<int>(
                "sp_Transactions_Transfer",
                new
                {
                    FromAccountNumber = fromAccount,
                    ToAccountNumber = toAccount,
                    Amount = amount
                },
                commandType: CommandType.StoredProcedure
            );

            return result == 1;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(string accountNumber)
        {
            using var conn = _db.CreateConnection();

            return await conn.QueryAsync<Transaction>(
                "sp_Transactions_GetHistory",
                new { AccountNumber = accountNumber },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            using var conn = _db.CreateConnection();

            return await conn.QueryAsync<Transaction>(
                "sp_Transactions_GetAll",
                commandType: CommandType.StoredProcedure
            );
        }
    }
}