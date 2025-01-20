using Blazored.LocalStorage;
using LMS.Blazor.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure HttpClient for API calls
builder.Services.AddHttpClient("LmsAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["LmsAPIBaseAddress"] ?? "https://localhost:7044");
});

builder.Services.AddHttpClient("BffClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BffClient"] ?? "https://localhost:7224");
});

// Add Authentication and Authorization
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();

// Add Local Storage
builder.Services.AddBlazoredLocalStorage();

// Register Services
//builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IApiService, ClientApiService>();

// Add Options
builder.Services.AddOptions();

await builder.Build().RunAsync();