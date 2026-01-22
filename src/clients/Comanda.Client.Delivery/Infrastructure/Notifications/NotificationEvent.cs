namespace Comanda.Client.Delivery.Infrastructure.Notifications;

/// <summary>
/// Represents a notification event received from the SignalR hub
/// </summary>
public record NotificationEvent(string Name, string Payload);

/// <summary>
/// Payload for order-related events
/// </summary>
public record OrderEventPayload(string OrderPublicId);

/// <summary>
/// Known event names from the notification hub
/// </summary>
public static class NotificationEventNames
{
    public const string OrderCreated = "order.created";
    public const string OrderAccepted = "order.accepted";
    public const string OrderPreparingStarted = "order.preparing.started";
    public const string OrderReady = "order.ready";
    public const string OrderDeliveryStarted = "order.delivery.started";
    public const string OrderDeliveryLocationAssigned = "order.delivery.location.assigned";
    public const string OrderDeliveryFinished = "order.delivery.finished";
    public const string OrderLineCreated = "order.line.created";
    public const string OrderCompleted = "order.completed";
    public const string OrderCancelled = "order.cancelled";

    /// <summary>
    /// Check if the event name is an order-related event
    /// </summary>
    public static bool IsOrderEvent(string eventName) =>
        eventName.StartsWith("order.", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Extract the order public ID from the payload JSON
    /// </summary>
    public static string? ExtractOrderPublicId(string payloadJson)
    {
        try
        {
            var payload = System.Text.Json.JsonSerializer.Deserialize<OrderEventPayload>(
                payloadJson,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return payload?.OrderPublicId;
        }
        catch
        {
            return null;
        }
    }
}







