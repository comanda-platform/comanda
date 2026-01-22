namespace Comanda.Application.Notifications.Events;

using Comanda.Application.Notifications;

public sealed record BatchCompletedEvent(
    string BatchPublicId,
    string ProductPublicId,
    int Yield,
    DateOnly ProductionDate) : INotification
{
    public string Name => "batch.completed";
    public object Payload => new { BatchPublicId, ProductPublicId, Yield, ProductionDate = ProductionDate.ToString("yyyy-MM-dd") };
}







