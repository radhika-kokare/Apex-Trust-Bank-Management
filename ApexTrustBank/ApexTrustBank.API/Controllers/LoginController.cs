using ApexTrustBank.BLL.Interfaces;
using ApexTrustBank.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApexTrustBank.Models.Response;
using ApexTrustBank.BLL.Helpers;

namespace ApexTrustBank.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var result = await _loginService.LoginAsync(loginDto);

            if (result == null)
                return Unauthorized("Invalid email or password");

            return Ok(new
            {
                result,
                Message = "Login successful"
            });
        }

        [HttpPost("SendOTP")]
        public async Task<IActionResult> SendOtp(string Email)
        {
            var otp = await _loginService.SendOTPAsync(Email);
            if (otp != null)
            {
                return Ok(new { message = "OTP sent to your email", otp });
            }
            return BadRequest(new { message = "User not found" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            var result = await _loginService.ResetPasswordAsync(resetPasswordDTO);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var result = await _loginService.ChangePasswordAsync(changePasswordDTO);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO user)
        {
            try
            {
                await _loginService.CreateUser(user);

                return Ok(new
                {
                    message = "User created successfully"
                });
            }
            catch (ApplicationException ex) when (ex.Message == "EMAIL_ALREADY_EXISTS")
            {
                return Conflict(new
                {
                    message = "Email already exists"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Internal server error"
                });
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Invalid request" });

            try
            {
                await _loginService.RegisterAsync(dto);

                return Ok(new
                {
                    success = true,
                    message = "User registered successfully"
                });
            }
            catch (ApplicationException ex) when (ex.Message == "EMAIL_ALREADY_EXISTS")
            {
                return Conflict(new
                {
                    success = false,
                    message = "Email already exists"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal server error"
                });
            }
        }

        //[HttpPost("migrate-passwords")]
        //public async Task<IActionResult> MigratePasswords()
        //{
        //    await _loginService.MigratePlainTextPasswordsAsync();
        //    return Ok("Password migration completed");
        //}


    }
}
    




