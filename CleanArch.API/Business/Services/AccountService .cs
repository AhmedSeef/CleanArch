using CleanArch.API.Business.Interfaces;
using CleanArch.API.DTOs;
using CleanArch.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArch.API.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> RegisterAsync(RegisterDto model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return new ServiceResponse<string> { IsSuccess = false, Message = "Registration failed." };

            return new ServiceResponse<string> { IsSuccess = true, Message = "User registered successfully!" };
        }

        public async Task<ServiceResponse<string>> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !(await _userManager.CheckPasswordAsync(user, model.Password)))
            {
                return new ServiceResponse<string> { IsSuccess = false, Message = "Invalid credentials." };
            }

            var token = GenerateJwtToken(user);
            return new ServiceResponse<string> { IsSuccess = true, Data = token };
        }

        public async Task<ServiceResponse<UserProfileDto>> GetProfileAsync(ClaimsPrincipal userPrincipal)
        {
            var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return new ServiceResponse<UserProfileDto> { IsSuccess = false, Message = "User not found." };

            return new ServiceResponse<UserProfileDto>
            {
                IsSuccess = true,
                Data = new UserProfileDto { Email = user.Email, UserName = user.UserName }
            };
        }

        public async Task<ServiceResponse<string>> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return new ServiceResponse<string> { IsSuccess = false, Message = "User not found." };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (!result.Succeeded)
                return new ServiceResponse<string> { IsSuccess = false, Message = "Password reset failed." };

            return new ServiceResponse<string> { IsSuccess = true, Message = "Password reset successfully!" };
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
