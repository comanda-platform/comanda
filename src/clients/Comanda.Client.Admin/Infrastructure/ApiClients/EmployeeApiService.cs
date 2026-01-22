using Comanda.Client.Admin.Infrastructure.Auth;
using Comanda.Client.Admin.Models;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class EmployeeApiService(HttpClient httpClient, TokenService tokenService, IConfiguration configuration)
    : AdminApiClient(httpClient, tokenService, configuration)
{
    public async Task<IEnumerable<EmployeeResponse>> GetEmployeesAsync(
        bool? activeOnly = null,
        string? searchTerm = null)
    {
        var queryParams = new List<string>();

        if (activeOnly.HasValue)
            queryParams.Add($"activeOnly={activeOnly.Value}");
        if (!string.IsNullOrEmpty(searchTerm))
            queryParams.Add($"searchTerm={searchTerm}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";

        return await GetAsync<IEnumerable<EmployeeResponse>>($"api/employees{query}") ?? [];
    }

    public async Task<EmployeeResponse?> GetEmployeeByIdAsync(string publicId)
    {
        return await GetAsync<EmployeeResponse>($"api/employees/{publicId}");
    }

    public async Task<EmployeeResponse?> CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        var response = await PostAsync("api/employees", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<EmployeeResponse>();
    }

    public async Task<bool> UpdateEmployeeAsync(string publicId, UpdateEmployeeRequest request)
    {
        var response = await PatchAsync($"api/employees/{publicId}", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ActivateEmployeeAsync(string publicId)
    {
        var response = await PostAsync($"api/employees/{publicId}/activate", new { });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeactivateEmployeeAsync(string publicId)
    {
        var response = await PostAsync($"api/employees/{publicId}/deactivate", new { });
        return response.IsSuccessStatusCode;
    }

    public async Task<ApiKeyResponse?> GenerateApiKeyAsync(string publicId)
    {
        var response = await PostAsync($"api/employees/{publicId}/generate-api-key", new { });

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ApiKeyResponse>();
    }

    public async Task<bool> RevokeApiKeyAsync(string publicId)
    {
        var response = await PostAsync($"api/employees/{publicId}/revoke-api-key", new { });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteEmployeeAsync(string publicId)
    {
        var response = await DeleteAsync($"api/employees/{publicId}");
        return response.IsSuccessStatusCode;
    }
}










