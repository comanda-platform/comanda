namespace Comanda.Client.Kitchen.Infrastructure.Services;

using Comanda.Client.Kitchen.Infrastructure.ApiClients;
using Comanda.Client.Kitchen.Infrastructure.ApiClients.ApiModels;
using Comanda.Shared.Enums;

/// <summary>
/// Local display status for cooking UI (maps from API BatchStatus)
/// </summary>
public enum CookingStatus
{
    NotStarted,
    InProgress,
    Completed
}

/// <summary>
/// Local batch state for UI display
/// </summary>
public record BatchState(
    string BatchPublicId,
    string ProductPublicId,
    string ProductName,
    CookingStatus Status,
    int? Yield,
    DateTime? StartedAt,
    DateTime? CompletedAt);

public class ProductionStateService
{
    private readonly IKitchenApiClient _apiClient;
    private readonly Dictionary<string, List<BatchState>> _batchesByProduct = new();
    private DailyMenuResponse? _currentMenu;
    private DateOnly _currentDate = DateOnly.FromDateTime(DateTime.Today);

    public event Action? OnStateChanged;

    public ProductionStateService(IKitchenApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public DateOnly CurrentDate => _currentDate;
    public DailyMenuResponse? CurrentMenu => _currentMenu;

    /// <summary>
    /// Gets all batches grouped by product for backward compatibility
    /// </summary>
    public IReadOnlyDictionary<string, BatchState> Batches =>
        _batchesByProduct.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.FirstOrDefault() ?? new BatchState("", kvp.Key, "", CookingStatus.NotStarted, null, null, null));

    /// <summary>
    /// Gets all batches for a specific product
    /// </summary>
    public IReadOnlyList<BatchState> GetBatchesForProduct(string productPublicId)
    {
        return _batchesByProduct.TryGetValue(productPublicId, out var batches)
            ? batches.AsReadOnly()
            : Array.Empty<BatchState>();
    }

    public async Task LoadMenuForDateAsync(DateOnly date)
    {
        _currentDate = date;
        _currentMenu = await _apiClient.GetDailyMenuByDateAsync(date);

        // Clear and reload batches from API
        _batchesByProduct.Clear();

        if (_currentMenu != null)
        {
            // Initialize empty batch lists for all menu items
            foreach (var item in _currentMenu.Items)
            {
                _batchesByProduct[item.ProductPublicId] = new List<BatchState>();
            }

            // Load existing batches from API
            var apiBatches = await _apiClient.GetBatchesByDailyMenuAsync(_currentMenu.PublicId);

            foreach (var apiBatch in apiBatches)
            {
                var productName = _currentMenu.Items
                    .FirstOrDefault(i => i.ProductPublicId == apiBatch.ProductPublicId)
                    ?.ProductName ?? "Unknown";

                var batchState = MapToBatchState(apiBatch, productName);

                if (!_batchesByProduct.ContainsKey(apiBatch.ProductPublicId))
                {
                    _batchesByProduct[apiBatch.ProductPublicId] = new List<BatchState>();
                }

                _batchesByProduct[apiBatch.ProductPublicId].Add(batchState);
            }
        }

        NotifyStateChanged();
    }

    /// <summary>
    /// Refresh batches from API without reloading the menu
    /// </summary>
    public async Task RefreshBatchesAsync()
    {
        if (_currentMenu == null)
            return;

        var apiBatches = await _apiClient.GetBatchesByDailyMenuAsync(_currentMenu.PublicId);

        // Clear and rebuild batch state
        foreach (var key in _batchesByProduct.Keys.ToList())
        {
            _batchesByProduct[key].Clear();
        }

        foreach (var apiBatch in apiBatches)
        {
            var productName = _currentMenu.Items
                .FirstOrDefault(i => i.ProductPublicId == apiBatch.ProductPublicId)
                ?.ProductName ?? "Unknown";

            var batchState = MapToBatchState(apiBatch, productName);

            if (!_batchesByProduct.ContainsKey(apiBatch.ProductPublicId))
            {
                _batchesByProduct[apiBatch.ProductPublicId] = new List<BatchState>();
            }

            _batchesByProduct[apiBatch.ProductPublicId].Add(batchState);
        }

        NotifyStateChanged();
    }

    public BatchState? GetBatchState(string productPublicId)
    {
        // Return the most recent active batch (InProgress) or the most recent batch
        if (!_batchesByProduct.TryGetValue(productPublicId, out var batches) || batches.Count == 0)
            return null;

        return batches.FirstOrDefault(b => b.Status == CookingStatus.InProgress)
               ?? batches.LastOrDefault();
    }

    public BatchState? GetBatchByPublicId(string batchPublicId)
    {
        return _batchesByProduct.Values
            .SelectMany(b => b)
            .FirstOrDefault(b => b.BatchPublicId == batchPublicId);
    }

    /// <summary>
    /// Start cooking - creates batch via API
    /// </summary>
    public async Task<string?> StartCookingAsync(string productPublicId)
    {
        if (_currentMenu == null)
            return null;

        var result = await _apiClient.StartBatchAsync(productPublicId, _currentMenu.PublicId, _currentDate);

        if (result == null)
            return null;

        var productName = _currentMenu.Items
            .FirstOrDefault(i => i.ProductPublicId == productPublicId)
            ?.ProductName ?? "Unknown";

        var batchState = MapToBatchState(result, productName);

        if (!_batchesByProduct.ContainsKey(productPublicId))
        {
            _batchesByProduct[productPublicId] = new List<BatchState>();
        }

        _batchesByProduct[productPublicId].Add(batchState);
        NotifyStateChanged();

        return result.PublicId;
    }

    /// <summary>
    /// Complete cooking - updates batch via API
    /// </summary>
    public async Task<bool> CompleteCookingAsync(string batchPublicId, int yield, string? notes = null)
    {
        var result = await _apiClient.CompleteBatchAsync(batchPublicId, yield, notes);

        if (result == null)
            return false;

        // Update local state
        foreach (var batches in _batchesByProduct.Values)
        {
            var index = batches.FindIndex(b => b.BatchPublicId == batchPublicId);
            if (index >= 0)
            {
                var productName = batches[index].ProductName;
                batches[index] = MapToBatchState(result, productName);
                NotifyStateChanged();
                return true;
            }
        }

        return true;
    }

    /// <summary>
    /// Legacy overload for backward compatibility - completes the active batch for a product
    /// </summary>
    public async Task<bool> CompleteCookingAsync(string productPublicId, int yield)
    {
        var activeBatch = GetBatchesForProduct(productPublicId)
            .FirstOrDefault(b => b.Status == CookingStatus.InProgress);

        if (activeBatch == null)
            return false;

        return await CompleteCookingAsync(activeBatch.BatchPublicId, yield);
    }

    public int GetTotalProducedForProduct(string productPublicId)
    {
        if (!_batchesByProduct.TryGetValue(productPublicId, out var batches))
            return 0;

        return batches
            .Where(b => b.Status == CookingStatus.Completed)
            .Sum(b => b.Yield ?? 0);
    }

    /// <summary>
    /// Get total produced from API (more accurate)
    /// </summary>
    public async Task<int> GetTotalProducedForProductAsync(string productPublicId)
    {
        return await _apiClient.GetTotalYieldAsync(productPublicId, _currentDate);
    }

    public bool HasActiveBatch(string productPublicId)
    {
        return GetBatchesForProduct(productPublicId)
            .Any(b => b.Status == CookingStatus.InProgress);
    }

    public async Task<RecipeResponse?> GetRecipeForProductAsync(string productPublicId)
    {
        return await _apiClient.GetRecipeByProductAsync(productPublicId);
    }

    private static BatchState MapToBatchState(ProductionBatchResponse apiBatch, string productName)
    {
        var status = apiBatch.Status switch
        {
            BatchStatus.InProgress => CookingStatus.InProgress,
            BatchStatus.Completed => CookingStatus.Completed,
            _ => CookingStatus.NotStarted
        };

        return new BatchState(
            apiBatch.PublicId,
            apiBatch.ProductPublicId,
            productName,
            status,
            apiBatch.Yield,
            apiBatch.StartedAt,
            apiBatch.CompletedAt);
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}







