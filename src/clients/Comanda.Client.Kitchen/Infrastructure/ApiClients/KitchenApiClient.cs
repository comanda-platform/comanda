namespace Comanda.Client.Kitchen.Infrastructure.ApiClients;

using System.Net.Http.Json;
using Comanda.Client.Kitchen.Infrastructure.ApiClients.ApiModels;
using Comanda.Client.Kitchen.Infrastructure.Auth;

public class KitchenApiClient : IKitchenApiClient
{
    private readonly HttpClient _httpClient;
    private readonly AuthStateProvider _authStateProvider;

    public KitchenApiClient(HttpClient httpClient, AuthStateProvider authStateProvider)
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
    public async Task<IEnumerable<OrderResponse>> GetActiveOrdersAsync()
    {
        await SetAuthHeadersAsync();

        var response = await _httpClient.GetAsync("api/orders?active=true");

        if (!response.IsSuccessStatusCode)
            return [];

        return await response.Content.ReadFromJsonAsync<IEnumerable<OrderResponse>>() ?? [];
    }

    public async Task<OrderResponse?> GetOrderAsync(string publicId)
    {
        await SetAuthHeadersAsync();

        var response = await _httpClient.GetAsync($"api/orders/{publicId}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<OrderResponse>();
    }

    public async Task<bool> AcceptOrderAsync(string publicId)
    {
        await SetAuthHeadersAsync();

        var request = new { status = "Accepted" };
        var response = await _httpClient.PatchAsJsonAsync($"api/orders/{publicId}", request);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> StartPreparingOrderAsync(string publicId)
    {
        await SetAuthHeadersAsync();

        var request = new { status = "Preparing" };
        var response = await _httpClient.PatchAsJsonAsync($"api/orders/{publicId}", request);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> MarkOrderReadyAsync(string publicId)
    {
        await SetAuthHeadersAsync();

        var request = new { status = "Ready" };
        var response = await _httpClient.PatchAsJsonAsync($"api/orders/{publicId}", request);

        return response.IsSuccessStatusCode;
    }

    // Order Line Prep
    public async Task<bool> StartLinePrepAsync(string linePublicId)
    {
        await SetAuthHeadersAsync();

        var request = new { status = "Preparing" };
        var response = await _httpClient.PatchAsJsonAsync($"api/orders/lines/{linePublicId}", request);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CompleteLinePlatingAsync(string linePublicId)
    {
        await SetAuthHeadersAsync();

        var request = new { status = "Plated" };
        var response = await _httpClient.PatchAsJsonAsync($"api/orders/lines/{linePublicId}", request);

        return response.IsSuccessStatusCode;
    }

    // Daily Menus
    public async Task<DailyMenuResponse?> GetDailyMenuByDateAsync(DateOnly date)
    {
        await SetAuthHeadersAsync();

        var response = await _httpClient.GetAsync($"api/daily-menus/by-date/{date:yyyy-MM-dd}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<DailyMenuResponse>();
    }

    public async Task<IEnumerable<DailyMenuResponse>> GetDailyMenusAsync(DateOnly? fromDate = null, DateOnly? toDate = null)
    {
        await SetAuthHeadersAsync();

        var url = "api/daily-menus";

        if (fromDate.HasValue || toDate.HasValue)
        {
            var queryParams = new List<string>();

            if (fromDate.HasValue)
                queryParams.Add($"fromDate={fromDate.Value:yyyy-MM-dd}");

            if (toDate.HasValue)
                queryParams.Add($"toDate={toDate.Value:yyyy-MM-dd}");

            url += "?" + string.Join("&", queryParams);
        }

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return [];

        return await response.Content.ReadFromJsonAsync<IEnumerable<DailyMenuResponse>>() ?? [];
    }

    // Recipes
    public async Task<RecipeResponse?> GetRecipeByProductAsync(string productPublicId)
    {
        await SetAuthHeadersAsync();

        var response = await _httpClient.GetAsync($"api/recipes/by-product/{productPublicId}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<RecipeResponse>();
    }

    // Sides
    public async Task<IEnumerable<DailySideAvailabilityResponse>> GetDailySideAvailabilityAsync(DateOnly date)
    {
        await SetAuthHeadersAsync();

        var response = await _httpClient.GetAsync($"api/sides/availability/{date:yyyy-MM-dd}");

        if (!response.IsSuccessStatusCode)
            return [];

        return await response.Content.ReadFromJsonAsync<IEnumerable<DailySideAvailabilityResponse>>() ?? [];
    }

    public async Task<IEnumerable<SideResponse>> GetSidesAsync()
    {
        await SetAuthHeadersAsync();

        var response = await _httpClient.GetAsync("api/sides");

        if (!response.IsSuccessStatusCode)
            return [];

        return await response.Content.ReadFromJsonAsync<IEnumerable<SideResponse>>() ?? [];
    }

    // Production Batches
    public async Task<IEnumerable<ProductionBatchResponse>> GetBatchesByDateAsync(DateOnly date)
    {
        await SetAuthHeadersAsync();

        var response = await _httpClient.GetAsync($"api/batches/by-date/{date:yyyy-MM-dd}");

        if (!response.IsSuccessStatusCode)
            return [];

        return await response.Content.ReadFromJsonAsync<IEnumerable<ProductionBatchResponse>>() ?? [];
    }

    public async Task<IEnumerable<ProductionBatchResponse>> GetBatchesByDailyMenuAsync(string dailyMenuPublicId)
    {
        await SetAuthHeadersAsync();

        var response = await _httpClient.GetAsync($"api/batches/by-daily-menu/{dailyMenuPublicId}");

        if (!response.IsSuccessStatusCode)
            return [];

        return await response.Content.ReadFromJsonAsync<IEnumerable<ProductionBatchResponse>>() ?? [];
    }

    public async Task<ProductionBatchResponse?> StartBatchAsync(string productPublicId, string dailyMenuPublicId, DateOnly productionDate)
    {
        await SetAuthHeadersAsync();

        var request = new StartBatchRequest(productPublicId, dailyMenuPublicId, productionDate);
        var response = await _httpClient.PostAsJsonAsync("api/batches", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ProductionBatchResponse>();
    }

    public async Task<ProductionBatchResponse?> CompleteBatchAsync(string batchPublicId, int yield, string? notes = null)
    {
        await SetAuthHeadersAsync();

        var request = new CompleteBatchRequest(yield, null, notes);
        var response = await _httpClient.PostAsJsonAsync($"api/batches/{batchPublicId}/complete", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ProductionBatchResponse>();
    }

    public async Task<int> GetTotalYieldAsync(string productPublicId, DateOnly date)
    {
        await SetAuthHeadersAsync();

        var response = await _httpClient.GetAsync($"api/batches/yield/{productPublicId}/{date:yyyy-MM-dd}");

        if (!response.IsSuccessStatusCode)
            return 0;

        var result = await response.Content.ReadFromJsonAsync<TotalYieldResponse>();
        return result?.TotalYield ?? 0;
    }
}







