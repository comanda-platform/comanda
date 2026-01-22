namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderDeliveryLocationAssignedEvent(string OrderPublicId) : INotification
{
    public string Name => "order.delivery.location.assigned";
    public object Payload => new { OrderPublicId };
}






