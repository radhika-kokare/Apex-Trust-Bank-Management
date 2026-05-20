using ApexTrustBank.DLL.Infrastructure;
using ApexTrustBank.DLL.Interfaces;
using ApexTrustBank.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace ApexTrustBank.DLL.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnectionFactory _db;

        public AccountRepository(IDbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<Account>(
       "sp_Accounts_GetAll",
       commandType: CommandType.StoredProcedure
       );
        }

        public async Task<Account?> GetAccountByIdAsync(int AccountId)
        {
            using var conn = _db.CreateConnection();

            return await conn.QueryFirstOrDefaultAsync<Account>(
                "sp_Accounts_GetById",
                new { AccountId = AccountId },
                commandType: CommandType.StoredProcedure
            );
        }


        public async Task<int> CreateAccountAsync(Account account)
        {
            using var conn = _db.CreateConnection();


            var parameters = new
            {
                AccountType = account.AccountType,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                FullName = account.FullName,
                Email = account.Email,
                Mobile = account.Mobile,
                Address = account.Address

            };

            return await conn.ExecuteScalarAsync<int>(
                "sp_Accounts_Create",
                 parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateAccountStatusAsync(int accountId, bool isActive)
        {
            using var conn = _db.CreateConnection();

            var rows = await conn.ExecuteScalarAsync<int>(
                "sp_Accounts_UpdateStatus",
                new { AccountId = accountId, IsActive = isActive },
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;
        }

        public async Task<decimal?> GetBalanceByAccountNumberAsync(string accountNumber)
        {
            using var conn = _db.CreateConnection();

            return await conn.ExecuteScalarAsync<decimal?>(
                "sp_Accounts_GetBalanceByAccountNumber",
                new { AccountNumber = accountNumber },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Account>> GetCustomerListAsync()
        {
            using var conn = _db.CreateConnection();

            return await conn.QueryAsync<Account>(
                "sp_Accounts_GetCustomerList",
                commandType: CommandType.StoredProcedure
            );
        }


    }
}

