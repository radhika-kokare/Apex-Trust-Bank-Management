using ApexTrustBank.DAL.Interfaces;
using ApexTrustBank.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApexTrustBank.Models.Response;
using Microsoft.EntityFrameworkCore;
using ApexTrustBank.BLL.Models;

namespace ApexTrustBank.Repository
{

    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;

        public LoginRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbcs");
        }


        public async Task<LoginResultDTO?> LoginAsync(LoginDTO dto)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<LoginResultDTO>(
                "dbo.sp_LoginUser",
                new
                {
                    Email = dto.Email,
                    Password = dto.Password
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int?> GetUserIdByEmailAsync(string email)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<int?>(
                "SELECT UserId FROM Users WHERE Email = @Email AND IsActive = 1",
                new { Email = email }
            );
        }


        public async Task UpdateOtpAsync(int userId, string otp, DateTime otpExpiry)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);

            await conn.ExecuteAsync(
                "dbo.sp_UpdateUserOtp",
                new
                {
                    UserId = userId,
                    Otp = otp,
                    OtpExpiry = otpExpiry
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);

            var result = await conn.ExecuteScalarAsync<string>(
                "dbo.sp_ResetUserPassword",
                new
                {
                    resetPasswordDTO.Email,
                    resetPasswordDTO.Otp,
                    resetPasswordDTO.NewPassword
                },
                commandType: CommandType.StoredProcedure
            );

            return result ?? "Something went wrong.";
        }


        public async Task<string> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);

            var result = await conn.ExecuteScalarAsync<string>(
                "dbo.sp_ChangeUserPassword",
                new
                {
                    changePasswordDTO.Email,
                    changePasswordDTO.CurrentPassword,
                    changePasswordDTO.NewPassword
                },
                commandType: CommandType.StoredProcedure
            );

            return result ?? "Something went wrong.";
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);

            return await conn.QueryFirstOrDefaultAsync<User>(
                "dbo.sp_GetUserByEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure
            );
        }

        public int Insert(UserDTO user)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();

            parameters.Add("@Name", user.Name);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.Password);
            parameters.Add("@IsActive", user.IsActive);
            parameters.Add("@CreatedDate", user.CreatedDate);
            parameters.Add("@Otp", user.Otp);
            parameters.Add("@ExpiryTime", user.ExpiryTime);
            parameters.Add("@OtpExpiry", user.Otpexpiry);

            parameters.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);


            try
            {
                conn.Execute(
                    "dbo.sp_InsertUser",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return parameters.Get<int>("@UserId");
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {

                throw new ApplicationException("EMAIL_ALREADY_EXISTS");
            }
        }

        public async Task RegisterAsync(RegisterUserDTO dto, string encryptedPassword)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);
            await conn.ExecuteAsync(
                "dbo.sp_RegisterUser",
                new
                {
                    dto.Email,
                    Password = encryptedPassword,
                    dto.Name
                },
                commandType: CommandType.StoredProcedure);
        }


        //public async Task<List<User>> GetAllUsersAsync()
        //{
        //    using IDbConnection conn = new SqlConnection(_connectionString);

        //    return (await conn.QueryAsync<User>(
        //        "SELECT UserId, Password FROM Users"
        //    )).ToList();
        //}

        //public async Task UpdatePasswordAsync(int userId, string encryptedPassword)
        //{
        //    using IDbConnection conn = new SqlConnection(_connectionString);

        //    await conn.ExecuteAsync(
        //        "UPDATE Users SET Password = @Password WHERE UserId = @UserId",
        //        new { Password = encryptedPassword, UserId = userId }
        //    );
        //}
    }
}



