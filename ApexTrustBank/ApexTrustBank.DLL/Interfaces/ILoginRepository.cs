using ApexTrustBank.BLL.Models;
using ApexTrustBank.Models.DTOs;
using ApexTrustBank.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApexTrustBank.DAL.Interfaces
{
    public interface ILoginRepository
    {
        Task<LoginResultDTO?> LoginAsync(LoginDTO dto);

        Task<int?> GetUserIdByEmailAsync(string email);

        Task UpdateOtpAsync(int userId, string otp, DateTime otpExpiry);

        Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);

        Task<string> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO);
        Task<User?> GetByEmailAsync(string email);
        int Insert(UserDTO user);
        Task RegisterAsync(RegisterUserDTO dto, string encryptedPassword);


        //Task<List<User>> GetAllUsersAsync();
        //Task UpdatePasswordAsync(int userId, string encryptedPassword);


    }
}
