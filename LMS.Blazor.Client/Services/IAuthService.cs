using LMS.Shared.DTOs;

namespace LMS.Blazor.Client.Services
{
    public interface IAuthService
    {
        bool IsAuthenticated { get; }
        string? Username { get; }
        Task<bool> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
        Task<bool> RegisterAsync(UserForRegistrationDto registrationDto);
    }
}