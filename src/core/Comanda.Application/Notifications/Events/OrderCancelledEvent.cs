namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderCancelledEvent(string OrderPublicId) : INotification
{
    public string Name => "order.cancelled";
    public object Payload => new { OrderPublicId };
}






