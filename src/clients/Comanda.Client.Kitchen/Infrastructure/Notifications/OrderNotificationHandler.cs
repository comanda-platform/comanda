namespace Comanda.Client.Kitchen.Infrastructure.Notifications;

using Comanda.Client.Kitchen.Infrastructure.Services;

/// <summary>
/// Handles order-related notifications from the SignalR hub and triggers
/// appropriate refreshes in the FulfillmentStateService
/// </summary>
public class OrderNotificationHandler : IDisposable
{
    private readonly NotificationHubService _hubService;
    private readonly FulfillmentStateService _fulfillmentState;
    private bool _isSubscribed;

    public OrderNotificationHandler(
        NotificationHubService hubService,
        FulfillmentStateService fulfillmentState)
    {
        _hubService = hubService;
        _fulfillmentState = fulfillmentState;
    }

    /// <summary>
    /// Start listening to notifications from the hub
    /// </summary>
    public void StartListening()
    {
        if (_isSubscribed)
            return;

        _hubService.OnNotificationReceived += HandleNotification;
        _isSubscribed = true;
    }

    /// <summary>
    /// Stop listening to notifications from the hub
    /// </summary>
    public void StopListening()
    {
        if (!_isSubscribed)
            return;

        _hubService.OnNotificationReceived -= HandleNotification;
        _isSubscribed = false;
    }

    private void HandleNotification(NotificationEvent notification)
    {
        // Only handle order events
        if (!NotificationEventNames.IsOrderEvent(notification.Name))
            return;

        var orderPublicId = NotificationEventNames.ExtractOrderPublicId(notification.Payload);

        if (string.IsNullOrEmpty(orderPublicId))
        {
            System.Diagnostics.Debug.WriteLine($"OrderNotificationHandler: Could not extract OrderPublicId from {notification.Name}");
            return;
        }

        System.Diagnostics.Debug.WriteLine($"OrderNotificationHandler: Handling {notification.Name} for order {orderPublicId}");

        // Fire-and-forget the refresh - we don't want to block the notification handler
        _ = Task.Run(async () =>
        {
            try
            {
                await _fulfillmentState.RefreshOrderAsync(orderPublicId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OrderNotificationHandler: Error refreshing order {orderPublicId} - {ex.Message}");
            }
        });
    }

    public void Dispose()
    {
        StopListening();
        GC.SuppressFinalize(this);
    }
}







