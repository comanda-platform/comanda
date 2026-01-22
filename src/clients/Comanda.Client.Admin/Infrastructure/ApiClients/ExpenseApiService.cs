using Comanda.Client.Admin.Infrastructure.Auth;
using Comanda.Client.Admin.Models;
using Comanda.Shared.Enums;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class ExpenseApiService(HttpClient httpClient, TokenService tokenService, IConfiguration configuration)
    : AdminApiClient(httpClient, tokenService, configuration)
{
    public async Task<IEnumerable<ExpenseResponse>> GetExpensesAsync(
        ExpenseType? type = null,
        bool? activeOnly = null,
        string? employeeId = null,
        string? locationId = null)
    {
        var queryParams = new List<string>();

        if (type.HasValue)
            queryParams.Add($"type={(int)type.Value}");
        if (activeOnly.HasValue)
            queryParams.Add($"activeOnly={activeOnly.Value}");
        if (!string.IsNullOrEmpty(employeeId))
            queryParams.Add($"employeeId={employeeId}");
        if (!string.IsNullOrEmpty(locationId))
            queryParams.Add($"locationId={locationId}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";

        return await GetAsync<IEnumerable<ExpenseResponse>>($"api/expenses{query}") ?? [];
    }

    public async Task<ExpenseResponse?> GetExpenseByIdAsync(string publicId)
    {
        return await GetAsync<ExpenseResponse>($"api/expenses/{publicId}");
    }

    public async Task<ExpenseResponse?> CreateExpenseAsync(CreateExpenseRequest request)
    {
        var response = await PostAsync("api/expenses", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ExpenseResponse>();
    }

    public async Task<bool> UpdateExpenseAsync(string publicId, UpdateExpenseRequest request)
    {
        var response = await PatchAsync($"api/expenses/{publicId}", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> EndExpenseAsync(string publicId, DateTime? effectiveTo = null)
    {
        var request = new EndExpenseRequest(effectiveTo);
        var response = await PostAsync($"api/expenses/{publicId}/end", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteExpenseAsync(string publicId)
    {
        var response = await DeleteAsync($"api/expenses/{publicId}");
        return response.IsSuccessStatusCode;
    }
}










