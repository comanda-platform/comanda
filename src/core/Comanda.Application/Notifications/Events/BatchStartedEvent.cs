namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record BatchStartedEvent(
    string BatchPublicId,
    string ProductPublicId,
    string ProductName,
    DateOnly ProductionDate) : INotification
{
    public string Name => "batch.started";
    public object Payload => new { BatchPublicId, ProductPublicId, ProductName, ProductionDate = ProductionDate.ToString("yyyy-MM-dd") };
}







