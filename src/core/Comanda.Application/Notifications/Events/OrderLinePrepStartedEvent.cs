namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderLinePrepStartedEvent(
    string OrderLinePublicId,
    string OrderPublicId) : INotification
{
    public string Name => "order.line.prep.started";
    
    public object Payload => new { 
        OrderLinePublicId, 
        OrderPublicId 
    };
}







