using System.Net.Http.Json;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using LMS.Shared.User;


namespace LMS.Blazor.Services
{
    public class AuthHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AuthHttpService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenStorage _tokenStorage;

        public AuthHttpService(
            IHttpClientFactory httpClientFactory,
            ILogger<AuthHttpService> logger,
            UserManager<ApplicationUser> userManager,
            ITokenStorage tokenStorage)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _userManager = userManager;
            _tokenStorage = tokenStorage;
        }

        public async Task<(bool success, string? errorMessage, TokenDto? tokens)> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return (false, "Failed to login. Please try again.", null);
                }

                var tokens = await GetTokensFromApi(user.UserName!, password);
                if (tokens == null)
                {
                    return (false, "Failed to login. Please try again.", null);
                }

                await _tokenStorage.StoreTokensAsync(user.Id, tokens);
                return (true, null, tokens);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login attempt");
                return (false, "An error occurred during login. Please try again.", null);
            }
        }

        private async Task<TokenDto?> GetTokensFromApi(string username, string password)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("LmsAPIClient");
                var response = await httpClient.PostAsJsonAsync("api/auth/login",
                    new UserForAuthDto(username, password));

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed login attempt for user: {Username}", username);
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<TokenDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tokens from API");
                return null;
            }
        }

        public async Task<bool> LogoutAsync(string userId)
        {
            try
            {
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                return false;
            }
        }
    }
}