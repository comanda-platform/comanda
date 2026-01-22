namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderReadyEvent(string OrderPublicId) : INotification
{
    public string Name => "order.ready";
    public object Payload => new { OrderPublicId };
}






