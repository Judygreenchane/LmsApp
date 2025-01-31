﻿using LMS.Blazor.Client.Pages;
using LMS.Blazor.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace LMS.Blazor.Controller;

[Route("proxy-endpoint")]
[ApiController]
public class ProxyController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorage _tokenService;

    public ProxyController(IHttpClientFactory httpClientFactory, ITokenStorage tokenService)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }


    //[HttpPost]
    //[HttpPut]
    //[HttpDelete]
    //[HttpPatch]
    [HttpGet("{*request}")]
    public async Task<IActionResult> Proxy(string request) //ToDo send endpoint uri here!
    {
        // string endpoint = "api/demoauth";
        string endpoint = $"api/{request}";
        //if (request == "allcourse")
        //{
        //    endpoint = "api/course/courselist";
        //}
        //if (request == "SelectedCourse")
        //{
        //    endpoint = "api/Course/4";
        //}
        //endpoint =  request;
        // var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // if (userId == null)
        //  return Unauthorized();


        //  var accessToken = await _tokenService.GetAccessTokenAsync(userId);

        //ToDo: Before continue look for expired accesstoken and call refresh enpoint instead.
        //Better with delegatinghandler or separate service to extract this logic!

        //if (string.IsNullOrEmpty(accessToken))
        //{
        //    return Unauthorized();
        //}
        var client = _httpClientFactory.CreateClient("LmsAPIClient");
        // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var targetUri = new Uri($"{client.BaseAddress}{endpoint}{Request.QueryString}");
        var method = new HttpMethod(Request.Method);
        var requestMessage = new HttpRequestMessage(method, targetUri);

        if (method != HttpMethod.Get && Request.ContentLength > 0)
        {
            requestMessage.Content = new StreamContent(Request.Body);
        }

        foreach (var header in Request.Headers)
        {
            if (!header.Key.Equals("Host", StringComparison.OrdinalIgnoreCase))
            {
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }
        }


        var response = await client.SendAsync(requestMessage);

        // if (!response.IsSuccessStatusCode)
        //     return Unauthorized();

        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }
}

