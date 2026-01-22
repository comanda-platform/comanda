namespace Comanda.Client.Delivery.Infrastructure.ApiClients;

using System.Net.Http.Json;
using Comanda.Client.Delivery.Infrastructure.ApiClients.ApiModels;
using Comanda.Client.Delivery.Infrastructure.Auth;
using Comanda.Shared.Enums;

public class DeliveryApiClient : IDeliveryApiClient
{
    private readonly HttpClient _httpClient;
    private readonly AuthStateProvider _authStateProvider;

    public DeliveryApiClient(HttpClient httpClient, AuthStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }

    private async Task SetAuthHeadersAsync()
    {
        var token = await _authStateProvider.GetTokenAsync();
        var apiKey = await _authStateProvider.GetApiKeyAsync();

        _httpClient.DefaultRequestHeaders.Clear();

        if (!string.IsNullOrEmpty(token))
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        if (!string.IsNullOrEmpty(apiKey))
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
    }

    // Auth
    public async Task<AuthResponse?> LoginAsync(string email, string password)
    {
        var request = new LoginRequest(email, password);
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AuthResponse>();
    }

    // Orders
    public async Task<IEnumerable<OrderResponse>> GetOrdersReadyForDeliveryAsync()
    {
        await SetAuthHeadersAsync();

        // Get orders with status Ready that are Delivery fulfillment type
        var response = await _httpClient.GetAsync($"api/orders?status={(int)OrderStatus.Ready}");

        if (!response.IsSuccessStatusCode)
            return [];

        var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderResponse>>() ?? [];

        // Filter to only delivery orders
        return orders.Where(o => o.FulfillmentType == OrderFulfillmentType.Delivery);
    }

    public async Task<OrderResponse?> GetOrderAsync(string publicId)
    {
        await SetAuthHeadersAsync();

        var response = await _httpClient.GetAsync($"api/orders/{publicId}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<OrderResponse>();
    }

    public async Task<bool> StartDeliveryAsync(string publicId)
    {
        await SetAuthHeadersAsync();

        var request = new { status = "InTransit" };
        var response = await _httpClient.PatchAsJsonAsync($"api/orders/{publicId}", request);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> MarkDeliveredAsync(string publicId)
    {
        await SetAuthHeadersAsync();

        var request = new { status = "Delivered" };
        var response = await _httpClient.PatchAsJsonAsync($"api/orders/{publicId}", request);

        return response.IsSuccessStatusCode;
    }

    // Locations
    public async Task<LocationResponse?> GetLocationAsync(string publicId)
    {
        await SetAuthHeadersAsync();

        var response = await _httpClient.GetAsync($"api/locations/{publicId}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LocationResponse>();
    }
}







