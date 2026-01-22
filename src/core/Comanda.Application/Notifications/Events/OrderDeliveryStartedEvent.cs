namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderDeliveryStartedEvent(string OrderPublicId) : INotification
{
    public string Name => "order.delivery.started";
    public object Payload => new { OrderPublicId };
}






