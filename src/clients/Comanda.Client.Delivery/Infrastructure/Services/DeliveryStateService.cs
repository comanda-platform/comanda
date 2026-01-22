namespace Comanda.Client.Delivery.Infrastructure.Services;

using Comanda.Client.Delivery.Infrastructure.ApiClients;
using Comanda.Client.Delivery.Infrastructure.ApiClients.ApiModels;

/// <summary>
/// Manages the state of orders ready for delivery
/// </summary>
public class DeliveryStateService
{
    private readonly IDeliveryApiClient _apiClient;
    private readonly List<DeliveryOrderInfo> _orders = [];
    private Timer? _pollingTimer;
    private bool _isPolling;

    public event Action? OnStateChanged;

    public IReadOnlyList<DeliveryOrderInfo> Orders => _orders;

    public DeliveryStateService(IDeliveryApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public void StartPolling(int intervalSeconds = 30)
    {
        if (_isPolling) return;

        _isPolling = true;
        _pollingTimer = new Timer(
            async _ => await RefreshOrdersAsync(),
            null,
            TimeSpan.FromSeconds(2),  // Delay first call by 2 seconds to allow app initialization
            TimeSpan.FromSeconds(intervalSeconds));

        System.Diagnostics.Debug.WriteLine(
            $"DeliveryStateService: Started polling with 2-second initial delay, then every {intervalSeconds} seconds");
    }

    public void StopPolling()
    {
        if (!_isPolling) return;

        _pollingTimer?.Dispose();
        _pollingTimer = null;
        _isPolling = false;

        System.Diagnostics.Debug.WriteLine("DeliveryStateService: Stopped polling");
    }

    public async Task RefreshOrdersAsync()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("DeliveryStateService: Refreshing orders...");

            var orders = await _apiClient.GetOrdersReadyForDeliveryAsync();
            var orderInfoList = new List<DeliveryOrderInfo>();

            foreach (var order in orders)
            {
                LocationResponse? location = null;

                if (!string.IsNullOrWhiteSpace(order.LocationPublicId))
                {
                    location = await _apiClient.GetLocationAsync(order.LocationPublicId);

                    // Defensive logging – real-world data WILL have missing coords
                    if (location is not null &&
                        (location.Latitude is null || location.Longitude is null))
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"Location {location.PublicId} has no coordinates, skipping map placement");
                    }
                }

                orderInfoList.Add(new DeliveryOrderInfo
                {
                    OrderPublicId = order.PublicId,
                    TotalItems = order.TotalItems,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status,
                    CreatedAt = order.CreatedAt,
                    Lines = order.Lines.ToList(),

                    LocationPublicId = order.LocationPublicId,
                    LocationName = location?.Name,
                    AddressLine = location?.AddressLine,

                    // These are now double?
                    Latitude = location?.Latitude,
                    Longitude = location?.Longitude
                });
            }

            _orders.Clear();
            _orders.AddRange(orderInfoList);

            System.Diagnostics.Debug.WriteLine(
                $"DeliveryStateService: Loaded {_orders.Count} orders ready for delivery");

            OnStateChanged?.Invoke();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"DeliveryStateService: Error refreshing orders - {ex}");
        }
    }

    public async Task<bool> StartDeliveryAsync(string orderPublicId)
    {
        var success = await _apiClient.StartDeliveryAsync(orderPublicId);

        if (success)
        {
            await RefreshOrdersAsync();
        }

        return success;
    }

    public async Task<bool> MarkDeliveredAsync(string orderPublicId)
    {
        var success = await _apiClient.MarkDeliveredAsync(orderPublicId);

        if (success)
        {
            await RefreshOrdersAsync();
        }

        return success;
    }
}


/// <summary>
/// Extended order info with location details for delivery display
/// </summary>
public class DeliveryOrderInfo
{
    public required string OrderPublicId { get; init; }
    public int TotalItems { get; init; }
    public decimal TotalAmount { get; init; }
    public Comanda.Shared.Enums.OrderStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }

    public List<OrderLineResponse> Lines { get; init; } = [];

    public string? LocationPublicId { get; init; }
    public string? LocationName { get; init; }
    public string? AddressLine { get; init; }

    // IMPORTANT: use double? for mapping
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }

    public bool HasCoordinates =>
        Latitude.HasValue && Longitude.HasValue;
}







