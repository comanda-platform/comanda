namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderAcceptedEvent(string OrderPublicId) : INotification
{
    public string Name => "order.accepted";
    public object Payload => new { OrderPublicId };
}






