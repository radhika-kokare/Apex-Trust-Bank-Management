using ApexTrustBank.Models.DTOs;
using System.Threading.Tasks;
using ApexTrustBank.Models.Response;

namespace ApexTrustBank.BLL.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResultDTO?> LoginAsync(LoginDTO dto);

        Task<string?> SendOTPAsync(string email);

        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);

        Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO);
        Task<int> CreateUser(UserDTO user);
        Task RegisterAsync(RegisterUserDTO dto);

        //Task MigratePlainTextPasswordsAsync();
    }
}






