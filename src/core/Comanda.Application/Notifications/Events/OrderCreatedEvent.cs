namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderCreatedEvent(string OrderPublicId) : INotification
{
    public string Name => "order.created";
    public object Payload => new { OrderPublicId };
}






