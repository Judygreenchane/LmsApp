using LMS.Blazor.Client.Models;
using LMS.Shared.DTOs;

namespace LMS.Blazor.Client.Services;

public interface IApiService
{

    Task<TResponse?> CallApiGetAsync<TResponse>(string endpoint);
    Task<T> CallApiPutAsync<T>(string endpoint, T data);
}
