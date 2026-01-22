using Comanda.Client.Admin.Infrastructure.Auth;
using Comanda.Client.Admin.Models;
using Comanda.Shared.Enums;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class BatchApiService(HttpClient httpClient, TokenService tokenService, IConfiguration configuration)
    : AdminApiClient(httpClient, tokenService, configuration)
{
    public async Task<IEnumerable<ProductionBatchResponse>> GetBatchesAsync(
        DateOnly? productionDate = null,
        BatchStatus? status = null,
        string? productId = null)
    {
        var queryParams = new List<string>();

        if (productionDate.HasValue)
            queryParams.Add($"productionDate={productionDate.Value:yyyy-MM-dd}");
        if (status.HasValue)
            queryParams.Add($"status={(int)status.Value}");
        if (!string.IsNullOrEmpty(productId))
            queryParams.Add($"productId={productId}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";

        return await GetAsync<IEnumerable<ProductionBatchResponse>>($"api/batches{query}") ?? [];
    }

    public async Task<ProductionBatchResponse?> GetBatchByIdAsync(string publicId)
    {
        return await GetAsync<ProductionBatchResponse>($"api/batches/{publicId}");
    }

    public async Task<ProductionBatchResponse?> CreateBatchAsync(CreateProductionBatchRequest request)
    {
        var response = await PostAsync("api/batches", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ProductionBatchResponse>();
    }

    public async Task<bool> CompleteBatchAsync(string publicId, CompleteBatchRequest request)
    {
        var response = await PostAsync($"api/batches/{publicId}/complete", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateNotesAsync(string publicId, string? notes)
    {
        var request = new UpdateBatchNotesRequest(notes);
        var response = await PatchAsync($"api/batches/{publicId}/notes", request);
        return response.IsSuccessStatusCode;
    }
}










