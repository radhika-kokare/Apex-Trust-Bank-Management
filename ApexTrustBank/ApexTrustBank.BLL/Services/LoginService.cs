using Azure.Core;
using ApexTrustBank.BLL.Helpers;
using ApexTrustBank.BLL.Interfaces;
using ApexTrustBank.DAL.Interfaces;
using ApexTrustBank.Repository;
using ApexTrustBank.Models.DTOs;
using ApexTrustBank.Models.Response;
using System.Net;
using System.Net.Mail;
using BCrypt.Net;
using ApexTrustBank.Helpers;

namespace ApexTrustBank.BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public LoginService(
            
            ILoginRepository loginRepository,
            JwtTokenHelper jwtTokenHelper)
        {
        

            _loginRepository = loginRepository
                ?? throw new ArgumentNullException(nameof(loginRepository));

            _jwtTokenHelper = jwtTokenHelper
                ?? throw new ArgumentNullException(nameof(jwtTokenHelper));
        }

        public async Task<LoginResultDTO?> LoginAsync(LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email is required");

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Password is required");

            var user = await _loginRepository.GetByEmailAsync(dto.Email.Trim());

            if (user == null)
                return null;


            var decryptPassword = EncryptionHelper.Decrypt(user.Password);


            if (decryptPassword != dto.Password)
                return null;


            var token = _jwtTokenHelper.GenerateToken(
                user.UserId,
                user.Email
            );

            return new LoginResultDTO
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                Token = token
            };
        }
        //public async Task<LoginResultDTO?> LoginAsync(LoginDTO dto)
        //{
        //    if (string.IsNullOrWhiteSpace(dto.Email))
        //        throw new ArgumentException("Email is required");

        //    if (string.IsNullOrWhiteSpace(dto.Password))
        //        throw new ArgumentException("Password is required");

        //    var user = await _loginRepository.GetByEmailAsync(dto.Email.Trim());

        //    if (user == null)
        //        return null;

        //    string dbPassword;

        //    // ✅ HANDLE BOTH PLAIN TEXT AND ENCRYPTED
        //    if (EncryptionHelper.IsBase64(user.Password))
        //    {
        //        dbPassword = EncryptionHelper.Decrypt(user.Password);
        //    }
        //    else
        //    {
        //        // old plain-text password
        //        dbPassword = user.Password;

        //        // 🔐 OPTIONAL: auto-migrate on successful login
        //        var encrypted = EncryptionHelper.Encrypt(user.Password);
        //        await _loginRepository.UpdatePasswordAsync(user.UserId, encrypted);
        //    }

        //    if (dbPassword != dto.Password)
        //        return null;

        //    var token = _jwtTokenHelper.GenerateToken(user.UserId, user.Email);

        //    return new LoginResultDTO
        //    {
        //        UserId = user.UserId,
        //        Email = user.Email,
        //        Name = user.Name,
        //        Token = token
        //    };
        //}


        public async Task<string?> SendOTPAsync(string email)
        {
            int? userId = await _loginRepository.GetUserIdByEmailAsync(email);

            if (userId == null)
                return null;

            string otp = GenerateOtp();
            DateTime otpExpiry = DateTime.Now.AddMinutes(10);


            await _loginRepository.UpdateOtpAsync(userId.Value, otp, otpExpiry);
            SendOtpEmail(email, otp);


            return otp;
        }

        private void SendOtpEmail(string toEmail, string otp)
        {
            var fromAddress = new MailAddress("radhikakokare29@gmail.com", "MyApp");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "cdoa hkvl cbdf tggc";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "OTP for Password Reset",
                Body = $"Your OTP is: {otp}"
            };

            smtp.Send(message);
        }

        private string GenerateOtp()
        {
            return new Random().Next(100000, 999999).ToString();
        }


        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            var message = await _loginRepository.ResetPasswordAsync(dto);

            return new ResetPasswordResponse
            {
                IsSuccess = message == "Password reset successful.",
                Message = message
            };
        }


        public async Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordDTO dto)
        {
            if (dto.NewPassword != dto.ConfirmNewPassword)
            {
                return new ChangePasswordResponse
                {
                    IsSuccess = false,
                    Message = "New password and confirm password do not match."
                };
            }

            var message = await _loginRepository.ChangePasswordAsync(dto);

            return new ChangePasswordResponse
            {
                IsSuccess = message == "Password changed successfully.",
                Message = message
            };
        }

        public Task<int> CreateUser(UserDTO user)
        {
          

            user.Password = EncryptionHelper.Encrypt(user.Password);

            user.CreatedDate = DateTime.Now;

            return Task.FromResult(_loginRepository.Insert(user));
        }

        //public async Task RegisterAsync(RegisterUserDTO dto)
        //{
        //    var existingUser = await _loginRepository.GetByEmailAsync(dto.Email);
        //    if (existingUser != null)
        //        throw new ApplicationException("EMAIL_ALREADY_EXISTS");

        //    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        //    //dto.Password = EncryptionHelper.Encrypt(dto.Password);


        //    await _loginRepository.RegisterAsync(dto, hashedPassword);
        //}
        // REGISTER
        public async Task RegisterAsync(RegisterUserDTO dto)
        {
          
            var existingUser = await _loginRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new ApplicationException("EMAIL_ALREADY_EXISTS");

            var encryptedPassword = EncryptionHelper.Encrypt(dto.Password);

            var userToRegister = new RegisterUserDTO
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = encryptedPassword
            };

            await _loginRepository.RegisterAsync(dto, encryptedPassword);
        }


        //public async Task MigratePlainTextPasswordsAsync()
        //{
        //    var users = await _loginRepository.GetAllUsersAsync();

        //    foreach (var user in users)
        //    {
        //        // encrypt only plain-text passwords
        //        if (!EncryptionHelper.IsBase64(user.Password))
        //        {
        //            var encrypted = EncryptionHelper.Encrypt(user.Password);
        //            await _loginRepository.UpdatePasswordAsync(user.UserId, encrypted);
        //        }
        //    }
        //}

    }
}

