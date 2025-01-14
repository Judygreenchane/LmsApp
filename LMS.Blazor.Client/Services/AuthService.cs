using System.Net.Http.Json;
using Blazored.LocalStorage;
using LMS.Shared.DTOs;
using LMS.Shared.User;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LMS.Blazor.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly CustomAuthStateProvider _authStateProvider;

        public AuthService(
            IHttpClientFactory httpClientFactory,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authStateProvider)
        {
            _http = httpClientFactory.CreateClient("LmsAPI");
            _localStorage = localStorage;
            _authStateProvider = (CustomAuthStateProvider)authStateProvider;
        }

        public bool IsAuthenticated
        {
            get
            {
                var authState = _authStateProvider.GetAuthenticationStateAsync().Result;
                return authState.User.Identity?.IsAuthenticated ?? false;
            }
        }

        public string? Username
        {
            get
            {
                var authState = _authStateProvider.GetAuthenticationStateAsync().Result;
                return authState.User.Identity?.Name;
            }
        }

        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/auth/login", loginDto);
                if (!response.IsSuccessStatusCode)
                    return false;

                var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                if (result?.Token == null)
                    return false;

                await _localStorage.SetItemAsync("authToken", result.Token);
                _authStateProvider.NotifyUserAuthentication(result.Token);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _authStateProvider.NotifyUserLogout();
        }

        public async Task<bool> RegisterAsync(UserForRegistrationDto registrationDto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/auth", registrationDto);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadFromJsonAsync<List<IdentityError>>();
                    // You might want to handle these errors in your UI
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }


}