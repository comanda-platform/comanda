namespace Comanda.Client.Kitchen.Infrastructure.Services;

using Comanda.Client.Kitchen.Infrastructure.ApiClients;
using Comanda.Client.Kitchen.Infrastructure.ApiClients.ApiModels;
using Comanda.Shared.Enums;

public record OrderLineState(
    string LinePublicId,
    string ProductPublicId,
    int Quantity,
    OrderLinePrepStatus PrepStatus,
    DateTime? PrepStartedAt,
    DateTime? PrepCompletedAt,
    string? ContainerType,
    List<string>? SelectedSides);

public record OrderState(
    string OrderPublicId,
    OrderFulfillmentType FulfillmentType,
    OrderStatus ApiStatus,
    List<OrderLineState> Lines,
    DateTime CreatedAt);

public class FulfillmentStateService
{
    private readonly IKitchenApiClient _apiClient;
    private readonly List<OrderState> _orders = new();
    private Timer? _pollingTimer;
    private bool _isPolling;

    public event Action? OnStateChanged;

    public FulfillmentStateService(IKitchenApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public IReadOnlyList<OrderState> Orders => _orders;

    public void StartPolling(int intervalSeconds = 10)
    {
        if (_isPolling)
            return;

        _isPolling = true;
        _pollingTimer = new Timer(
            async _ => await RefreshOrdersAsync(),
            null,
            TimeSpan.FromSeconds(2), // Delay first call by 2 seconds to allow app initialization
            TimeSpan.FromSeconds(intervalSeconds));
    }

    public void StopPolling()
    {
        _isPolling = false;
        _pollingTimer?.Dispose();
        _pollingTimer = null;
    }

    public async Task RefreshOrdersAsync()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("FulfillmentStateService: Refreshing orders...");

            var apiOrders = await _apiClient.GetActiveOrdersAsync();

            _orders.Clear();

            foreach (var apiOrder in apiOrders)
            {
                var lines = apiOrder.Lines.Select(l => new OrderLineState(
                    l.PublicId,
                    l.ProductPublicId,
                    l.Quantity,
                    l.PrepStatus,
                    l.PrepStartedAt,
                    l.PrepCompletedAt,
                    l.ContainerType,
                    l.SelectedSides?.ToList())).ToList();

                _orders.Add(new OrderState(
                    apiOrder.PublicId,
                    apiOrder.FulfillmentType,
                    apiOrder.Status,
                    lines,
                    apiOrder.CreatedAt));
            }

            System.Diagnostics.Debug.WriteLine($"FulfillmentStateService: Loaded {_orders.Count} active orders");

            NotifyStateChanged();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"FulfillmentStateService: Error refreshing orders - {ex.Message}");
            // Continue operating with existing orders
        }
    }

    /// <summary>
    /// Refresh a single order by fetching it from the API.
    /// If the order is no longer active (completed/cancelled), it will be removed from the list.
    /// If the order is new, it will be added to the list.
    /// </summary>
    public async Task RefreshOrderAsync(string orderPublicId)
    {
        var apiOrder = await _apiClient.GetOrderAsync(orderPublicId);

        if (apiOrder == null)
        {
            // Order not found or no longer accessible - remove from local state
            _orders.RemoveAll(o => o.OrderPublicId == orderPublicId);
            NotifyStateChanged();
            return;
        }

        // Check if order is still active (not completed or cancelled)
        var isActive = apiOrder.Status is not (OrderStatus.Completed or OrderStatus.Cancelled);

        var existingIndex = _orders.FindIndex(o => o.OrderPublicId == orderPublicId);

        if (!isActive)
        {
            // Order is no longer active - remove from local state
            if (existingIndex >= 0)
            {
                _orders.RemoveAt(existingIndex);
                NotifyStateChanged();
            }
            return;
        }

        // Map API order to local state
        var lines = apiOrder.Lines.Select(l => new OrderLineState(
            l.PublicId,
            l.ProductPublicId,
            l.Quantity,
            l.PrepStatus,
            l.PrepStartedAt,
            l.PrepCompletedAt,
            l.ContainerType,
            l.SelectedSides?.ToList())).ToList();

        var orderState = new OrderState(
            apiOrder.PublicId,
            apiOrder.FulfillmentType,
            apiOrder.Status,
            lines,
            apiOrder.CreatedAt);

        if (existingIndex >= 0)
        {
            _orders[existingIndex] = orderState;
        }
        else
        {
            _orders.Add(orderState);
        }

        NotifyStateChanged();
    }

    public async Task<bool> AcceptOrderAsync(string orderPublicId)
    {
        var success = await _apiClient.AcceptOrderAsync(orderPublicId);

        if (success)
            await RefreshOrderAsync(orderPublicId);

        return success;
    }

    public async Task<bool> StartPreparingOrderAsync(string orderPublicId)
    {
        var success = await _apiClient.StartPreparingOrderAsync(orderPublicId);

        if (success)
            await RefreshOrderAsync(orderPublicId);

        return success;
    }

    public async Task<bool> MarkOrderReadyAsync(string orderPublicId)
    {
        var success = await _apiClient.MarkOrderReadyAsync(orderPublicId);

        if (success)
            await RefreshOrderAsync(orderPublicId);

        return success;
    }

    /// <summary>
    /// Start preparation of an order line via API
    /// </summary>
    public async Task<bool> StartPrepLineAsync(string orderPublicId, string linePublicId)
    {
        var success = await _apiClient.StartLinePrepAsync(linePublicId);

        if (success)
            await RefreshOrderAsync(orderPublicId);

        return success;
    }

    /// <summary>
    /// Complete plating of an order line via API (container and sides already set at order creation)
    /// </summary>
    public async Task<bool> CompleteLineAsync(string orderPublicId, string linePublicId)
    {
        var success = await _apiClient.CompleteLinePlatingAsync(linePublicId);

        if (success)
            await RefreshOrderAsync(orderPublicId);

        return success;
    }

    public bool AreAllLinesPlated(string orderPublicId)
    {
        var order = _orders.FirstOrDefault(o => o.OrderPublicId == orderPublicId);

        return order?.Lines.All(l => l.PrepStatus == OrderLinePrepStatus.Plated) ?? false;
    }

    public int GetCommittedQuantityForProduct(string productPublicId)
    {
        return _orders
            .SelectMany(o => o.Lines)
            .Where(l => l.ProductPublicId == productPublicId &&
                        l.PrepStatus != OrderLinePrepStatus.Completed)
            .Sum(l => l.Quantity);
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}







