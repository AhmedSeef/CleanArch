using CleanArch.API.Business.Interfaces;
using CleanArch.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _accountService.RegisterAsync(model);
            return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
        }

        // Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _accountService.LoginAsync(model);
            return result.IsSuccess ? Ok(result.Data) : Unauthorized(result.Message);
        }

        // Get Profile
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _accountService.GetProfileAsync(User);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

        // Reset Password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var result = await _accountService.ResetPasswordAsync(model);
            return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
        }
    }
}
