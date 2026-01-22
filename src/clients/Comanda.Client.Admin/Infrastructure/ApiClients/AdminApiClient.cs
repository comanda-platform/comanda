using Comanda.Client.Admin.Infrastructure.Auth;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class AdminApiClient(
    HttpClient httpClient,
    TokenService tokenService,
    IConfiguration configuration)
{
    protected readonly HttpClient HttpClient = httpClient;
    protected readonly TokenService TokenService = tokenService;
    protected readonly string BaseUrl = configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7001";

    protected async Task SetAuthHeadersAsync()
    {
        var token = await TokenService.GetTokenAsync();
        var apiKey = await TokenService.GetApiKeyAsync();

        HttpClient.DefaultRequestHeaders.Clear();

        if (!string.IsNullOrEmpty(token))
        {
            HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        if (!string.IsNullOrEmpty(apiKey))
        {
            HttpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
        }
    }

    protected async Task<T?> GetAsync<T>(string endpoint)
    {
        await SetAuthHeadersAsync();
        var response = await HttpClient.GetAsync($"{BaseUrl}/{endpoint}");

        if (!response.IsSuccessStatusCode)
            return default;

        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
    {
        await SetAuthHeadersAsync();
        return await HttpClient.PostAsJsonAsync($"{BaseUrl}/{endpoint}", data);
    }

    protected async Task<HttpResponseMessage> PatchAsync<T>(string endpoint, T data)
    {
        await SetAuthHeadersAsync();
        return await HttpClient.PatchAsJsonAsync($"{BaseUrl}/{endpoint}", data);
    }

    protected async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        await SetAuthHeadersAsync();
        return await HttpClient.DeleteAsync($"{BaseUrl}/{endpoint}");
    }
}










