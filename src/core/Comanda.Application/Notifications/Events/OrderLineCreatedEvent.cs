namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderLineCreatedEvent(
    string OrderPublicId,
    string OrderLinePublicId) : INotification
{
    public string Name => "order.line.created";
    
    public object Payload => new { 
        OrderLinePublicId, 
        OrderPublicId 
    };
}






