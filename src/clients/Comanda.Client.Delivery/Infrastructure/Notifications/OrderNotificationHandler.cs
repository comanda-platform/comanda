namespace Comanda.Client.Delivery.Infrastructure.Notifications;

using Comanda.Client.Delivery.Infrastructure.Services;

/// <summary>
/// Handles order-related notifications and updates the delivery state
/// </summary>
public class OrderNotificationHandler
{
    private readonly NotificationHubService _hubService;
    private readonly DeliveryStateService _deliveryState;
    private bool _isListening;

    public OrderNotificationHandler(
        NotificationHubService hubService,
        DeliveryStateService deliveryState)
    {
        _hubService = hubService;
        _deliveryState = deliveryState;
    }

    public void StartListening()
    {
        if (_isListening) return;

        _hubService.OnNotificationReceived += HandleNotification;
        _isListening = true;
        System.Diagnostics.Debug.WriteLine("OrderNotificationHandler: Started listening for notifications");
    }

    public void StopListening()
    {
        if (!_isListening) return;

        _hubService.OnNotificationReceived -= HandleNotification;
        _isListening = false;
        System.Diagnostics.Debug.WriteLine("OrderNotificationHandler: Stopped listening for notifications");
    }

    private void HandleNotification(NotificationEvent notification)
    {
        System.Diagnostics.Debug.WriteLine($"OrderNotificationHandler: Handling notification - {notification.Name}");

        // Extract order ID from payload
        var orderPublicId = NotificationEventNames.ExtractOrderPublicId(notification.Payload);

        if (string.IsNullOrEmpty(orderPublicId))
        {
            System.Diagnostics.Debug.WriteLine("OrderNotificationHandler: Could not extract order ID from payload");
            return;
        }

        // Handle delivery-relevant events
        switch (notification.Name)
        {
            case NotificationEventNames.OrderReady:
                // New order is ready for delivery - refresh the list
                System.Diagnostics.Debug.WriteLine($"OrderNotificationHandler: Order {orderPublicId} is ready for delivery");
                _ = _deliveryState.RefreshOrdersAsync();
                break;

            case NotificationEventNames.OrderDeliveryStarted:
            case NotificationEventNames.OrderDeliveryFinished:
            case NotificationEventNames.OrderCompleted:
            case NotificationEventNames.OrderCancelled:
                // Order state changed - refresh the specific order or list
                System.Diagnostics.Debug.WriteLine($"OrderNotificationHandler: Order {orderPublicId} state changed ({notification.Name})");
                _ = _deliveryState.RefreshOrdersAsync();
                break;

            default:
                System.Diagnostics.Debug.WriteLine($"OrderNotificationHandler: Ignoring event {notification.Name}");
                break;
        }
    }
}







