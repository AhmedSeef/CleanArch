using CleanArch.API.DTOs;
using CleanArch.API.Models;
using System.Security.Claims;

namespace CleanArch.API.Business.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResponse<string>> RegisterAsync(RegisterDto model);
        Task<ServiceResponse<string>> LoginAsync(LoginDto model);
        Task<ServiceResponse<UserProfileDto>> GetProfileAsync(ClaimsPrincipal userPrincipal);
        Task<ServiceResponse<string>> ResetPasswordAsync(ResetPasswordDto model);
    }
}
