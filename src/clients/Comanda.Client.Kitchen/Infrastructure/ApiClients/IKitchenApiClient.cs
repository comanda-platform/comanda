namespace Comanda.Client.Kitchen.Infrastructure.ApiClients;

using Comanda.Client.Kitchen.Infrastructure.ApiClients.ApiModels;

public interface IKitchenApiClient
{
    // Auth
    Task<AuthResponse?> LoginAsync(string email, string password);

    // Orders
    Task<IEnumerable<OrderResponse>> GetActiveOrdersAsync();
    Task<OrderResponse?> GetOrderAsync(string publicId);
    Task<bool> AcceptOrderAsync(string publicId);
    Task<bool> StartPreparingOrderAsync(string publicId);
    Task<bool> MarkOrderReadyAsync(string publicId);
    
    // Order Line Prep
    Task<bool> StartLinePrepAsync(string linePublicId);
    Task<bool> CompleteLinePlatingAsync(string linePublicId);

    // Daily Menus
    Task<DailyMenuResponse?> GetDailyMenuByDateAsync(DateOnly date);
    Task<IEnumerable<DailyMenuResponse>> GetDailyMenusAsync(DateOnly? fromDate = null, DateOnly? toDate = null);

    // Recipes
    Task<RecipeResponse?> GetRecipeByProductAsync(string productPublicId);

    // Sides
    Task<IEnumerable<DailySideAvailabilityResponse>> GetDailySideAvailabilityAsync(DateOnly date);
    Task<IEnumerable<SideResponse>> GetSidesAsync();

    // Production Batches
    Task<IEnumerable<ProductionBatchResponse>> GetBatchesByDateAsync(DateOnly date);
    Task<IEnumerable<ProductionBatchResponse>> GetBatchesByDailyMenuAsync(string dailyMenuPublicId);
    Task<ProductionBatchResponse?> StartBatchAsync(string productPublicId, string dailyMenuPublicId, DateOnly productionDate);
    Task<ProductionBatchResponse?> CompleteBatchAsync(string batchPublicId, int yield, string? notes = null);
    Task<int> GetTotalYieldAsync(string productPublicId, DateOnly date);
}







