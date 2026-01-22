namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderCompletedEvent(string OrderPublicId) : INotification
{
    public string Name => "order.completed";
    public object Payload => new { OrderPublicId };
}






