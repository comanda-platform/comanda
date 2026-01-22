namespace Comanda.Application.Notifications;

public interface INotificationQueue
{
    ValueTask EnqueueAsync(INotification notification);
    IAsyncEnumerable<INotification> DequeueAsync(CancellationToken ct);
}






