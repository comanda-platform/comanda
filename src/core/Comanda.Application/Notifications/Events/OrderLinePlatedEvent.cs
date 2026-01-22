namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record OrderLinePlatedEvent(
    string OrderLinePublicId,
    string OrderPublicId,
    string ContainerType,
    IReadOnlyList<string>? SelectedSides) : INotification
{
    public string Name => "order.line.plated";
    
    public object Payload => new { 
        OrderLinePublicId, 
        OrderPublicId,
        ContainerType,
        SelectedSides
    };
}







