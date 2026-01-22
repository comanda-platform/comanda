namespace Comanda.Client.Kitchen.Infrastructure.Services;

public enum AvailabilityLevel
{
    Good,       // > 10 remaining
    Low,        // 4-10 remaining
    Critical,   // 1-3 remaining
    SoldOut     // 0 remaining
}

public record AvailabilityInfo(
    string ProductPublicId,
    string ProductName,
    int Produced,
    int Committed,
    int Remaining,
    AvailabilityLevel Level);

public class AvailabilityStateService
{
    private readonly ProductionStateService _productionService;
    private readonly FulfillmentStateService _fulfillmentService;
    private readonly Dictionary<string, AvailabilityInfo> _availability = new();

    public event Action? OnAvailabilityChanged;

    public AvailabilityStateService(
        ProductionStateService productionService,
        FulfillmentStateService fulfillmentService)
    {
        _productionService = productionService;
        _fulfillmentService = fulfillmentService;

        // Subscribe to state changes from both services
        _productionService.OnStateChanged += RecalculateAvailability;
        _fulfillmentService.OnStateChanged += RecalculateAvailability;
    }

    public IReadOnlyDictionary<string, AvailabilityInfo> Availability => _availability;

    public AvailabilityInfo? GetAvailability(string productPublicId)
    {
        return _availability.TryGetValue(productPublicId, out var info) ? info : null;
    }

    public int GetRemainingCount(string productPublicId)
    {
        var produced = _productionService.GetTotalProducedForProduct(productPublicId);
        var committed = _fulfillmentService.GetCommittedQuantityForProduct(productPublicId);

        return Math.Max(0, produced - committed);
    }

    public AvailabilityLevel GetAvailabilityLevel(int remaining)
    {
        return remaining switch
        {
            0 => AvailabilityLevel.SoldOut,
            <= 3 => AvailabilityLevel.Critical,
            <= 10 => AvailabilityLevel.Low,
            _ => AvailabilityLevel.Good
        };
    }

    public void RecalculateAvailability()
    {
        _availability.Clear();

        // Get all products from menu items in production state
        if (_productionService.CurrentMenu != null)
        {
            foreach (var item in _productionService.CurrentMenu.Items)
            {
                var productPublicId = item.ProductPublicId;
                var produced = _productionService.GetTotalProducedForProduct(productPublicId);
                var committed = _fulfillmentService.GetCommittedQuantityForProduct(productPublicId);
                var remaining = Math.Max(0, produced - committed);
                var level = GetAvailabilityLevel(remaining);

                _availability[productPublicId] = new AvailabilityInfo(
                    productPublicId,
                    item.OverriddenName ?? item.ProductName,
                    produced,
                    committed,
                    remaining,
                    level);
            }
        }

        OnAvailabilityChanged?.Invoke();
    }

    public bool HasLowAvailabilityItems()
    {
        return _availability.Values.Any(a =>
            a.Level == AvailabilityLevel.Low ||
            a.Level == AvailabilityLevel.Critical);
    }

    public bool HasSoldOutItems()
    {
        return _availability.Values.Any(a => a.Level == AvailabilityLevel.SoldOut);
    }

    public int GetLowAvailabilityCount()
    {
        return _availability.Values.Count(a =>
            a.Level == AvailabilityLevel.Low ||
            a.Level == AvailabilityLevel.Critical ||
            a.Level == AvailabilityLevel.SoldOut);
    }

    public IEnumerable<AvailabilityInfo> GetItemsByLevel(AvailabilityLevel level)
    {
        return _availability.Values.Where(a => a.Level == level);
    }
}







