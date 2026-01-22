using Comanda.Client.Admin.Infrastructure.Auth;
using Comanda.Client.Admin.Models;
using Comanda.Shared.Enums;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class LedgerApiService(HttpClient httpClient, TokenService tokenService, IConfiguration configuration)
    : AdminApiClient(httpClient, tokenService, configuration)
{
    public async Task<IEnumerable<LedgerEntryResponse>> GetLedgerEntriesAsync(
        string? clientId = null,
        LedgerEntryType? type = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(clientId))
            queryParams.Add($"clientId={clientId}");
        if (type.HasValue)
            queryParams.Add($"type={(int)type.Value}");
        if (from.HasValue)
            queryParams.Add($"from={from.Value:yyyy-MM-dd}");
        if (to.HasValue)
            queryParams.Add($"to={to.Value:yyyy-MM-dd}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";

        return await GetAsync<IEnumerable<LedgerEntryResponse>>($"api/ledger{query}") ?? [];
    }

    public async Task<LedgerEntryResponse?> GetLedgerEntryByIdAsync(string publicId)
    {
        return await GetAsync<LedgerEntryResponse>($"api/ledger/{publicId}");
    }

    public async Task<decimal> GetClientBalanceAsync(string clientId)
    {
        var balance = await GetAsync<decimal>($"api/ledger/clients/{clientId}/balance");
        return balance;
    }

    public async Task<LedgerEntryResponse?> CreateCreditAsync(CreateCreditRequest request)
    {
        var response = await PostAsync("api/ledger/credits", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LedgerEntryResponse>();
    }

    public async Task<LedgerEntryResponse?> CreatePaymentAsync(CreatePaymentRequest request)
    {
        var response = await PostAsync("api/ledger/payments", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LedgerEntryResponse>();
    }

    public async Task<LedgerEntryResponse?> CreateAdjustmentAsync(CreateAdjustmentRequest request)
    {
        var response = await PostAsync("api/ledger/adjustments", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LedgerEntryResponse>();
    }

    public async Task<LedgerEntryResponse?> CreateWriteOffAsync(CreateWriteOffRequest request)
    {
        var response = await PostAsync("api/ledger/writeoffs", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LedgerEntryResponse>();
    }
}










