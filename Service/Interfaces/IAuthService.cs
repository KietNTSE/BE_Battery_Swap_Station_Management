using BusinessObject.DTOs;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<UserProfileDto?> GetUserProfileAsync(string userId);
        string GenerateJwtToken(string userId, string email, string role);
    }
}