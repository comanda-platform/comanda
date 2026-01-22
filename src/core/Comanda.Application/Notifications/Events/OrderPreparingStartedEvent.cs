namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderPreparingStartedEvent(string OrderPublicId) : INotification
{
    public string Name => "order.preparing.started";
    public object Payload => new { OrderPublicId };
}






