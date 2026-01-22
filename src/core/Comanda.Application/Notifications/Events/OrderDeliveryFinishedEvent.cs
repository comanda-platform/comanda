namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderDeliveryFinishedEvent(string OrderPublicId) : INotification
{
    public string Name => "order.delivery.finished";
    public object Payload => new { OrderPublicId };
}






