using Comanda.Client.Admin.Infrastructure.Auth;
using Comanda.Client.Admin.Models;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class AccountApiService(HttpClient httpClient, TokenService tokenService, IConfiguration configuration)
    : AdminApiClient(httpClient, tokenService, configuration)
{
    public async Task<IEnumerable<AccountResponse>> GetAccountsAsync(bool? withCreditLine = null)
    {
        var query = withCreditLine.HasValue ? $"?withCreditLine={withCreditLine.Value}" : "";
        return await GetAsync<IEnumerable<AccountResponse>>($"api/accounts{query}") ?? [];
    }

    public async Task<AccountResponse?> GetAccountByIdAsync(string publicId)
    {
        return await GetAsync<AccountResponse>($"api/accounts/{publicId}");
    }

    public async Task<AccountResponse?> CreateAccountAsync(CreateAccountRequest request)
    {
        var response = await PostAsync("api/accounts", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AccountResponse>();
    }

    public async Task<bool> UpdateAccountAsync(string publicId, PatchAccountRequest request)
    {
        var response = await PatchAsync($"api/accounts/{publicId}", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAccountAsync(string publicId)
    {
        var response = await DeleteAsync($"api/accounts/{publicId}");
        return response.IsSuccessStatusCode;
    }
}
