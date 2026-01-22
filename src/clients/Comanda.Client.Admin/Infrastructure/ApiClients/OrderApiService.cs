using Comanda.Client.Admin.Infrastructure.Auth;
using Comanda.Client.Admin.Models;
using Comanda.Shared.Enums;

namespace Comanda.Client.Admin.Infrastructure.ApiClients;

public class OrderApiService(HttpClient httpClient, TokenService tokenService, IConfiguration configuration)
    : AdminApiClient(httpClient, tokenService, configuration)
{
    public async Task<IEnumerable<OrderResponse>> GetOrdersAsync(
        bool? active = null,
        OrderStatus? status = null,
        string? clientId = null,
        string? clientGroupId = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var queryParams = new List<string>();

        if (active.HasValue)
            queryParams.Add($"active={active.Value}");
        if (status.HasValue)
            queryParams.Add($"status={( int)status.Value}");
        if (!string.IsNullOrEmpty(clientId))
            queryParams.Add($"clientId={clientId}");
        if (!string.IsNullOrEmpty(clientGroupId))
            queryParams.Add($"clientGroupId={clientGroupId}");
        if (from.HasValue)
            queryParams.Add($"from={from.Value:yyyy-MM-dd}");
        if (to.HasValue)
            queryParams.Add($"to={to.Value:yyyy-MM-dd}");

        var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";

        return await GetAsync<IEnumerable<OrderResponse>>($"api/orders{query}") ?? [];
    }

    public async Task<OrderResponse?> GetOrderByIdAsync(string publicId)
    {
        return await GetAsync<OrderResponse>($"api/orders/{publicId}");
    }

    public async Task<OrderResponse?> CreateOrderAsync(CreateOrderRequest request)
    {
        var response = await PostAsync("api/orders", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<OrderResponse>();
    }

    public async Task<bool> AddOrderLineAsync(string orderPublicId, AddOrderLineRequest request)
    {
        var response = await PostAsync($"api/orders/{orderPublicId}/lines", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateOrderStatusAsync(string publicId, OrderStatus status, string? changedBy = null)
    {
        var request = new PatchOrderRequest(status, null, changedBy);
        var response = await PatchAsync($"api/orders/{publicId}", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateOrderLineStatusAsync(string linePublicId, OrderLineStatus status)
    {
        var request = new PatchOrderLineRequest(status);
        var response = await PatchAsync($"api/orders/lines/{linePublicId}", request);
        return response.IsSuccessStatusCode;
    }
}










