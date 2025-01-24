using LMS.Blazor.Client.Models;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace LMS.Blazor.Client.Services;

public class ClientApiService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager) : IApiService
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient("BffClient");

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    //ToDo: Make generic
    public async Task<TResponse?> CallApiGetAsync<TResponse>(string endpoint)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"proxy-endpoint/{endpoint}");
        var response = await httpClient.SendAsync(requestMessage);

        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden
           || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            navigationManager.NavigateTo("AccessDenied");
        }

        response.EnsureSuccessStatusCode();

        var demoDtos = await JsonSerializer.DeserializeAsync<TResponse>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions, CancellationToken.None);
        return demoDtos;
    }

    public async Task<T> CallApiPutAsync<T>(string endpoint, T data)
    {
        var jsonContent = JsonSerializer.Serialize(data, _jsonSerializerOptions);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"proxy-endpoint/{endpoint}")
        {
            Content = content
        };

        var response = await httpClient.SendAsync(requestMessage);

        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden
            || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            navigationManager.NavigateTo("AccessDenied");
        }

        response.EnsureSuccessStatusCode();

        // Deserialize the response content into type T
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(responseContent, _jsonSerializerOptions);

        return result;
    }
}
