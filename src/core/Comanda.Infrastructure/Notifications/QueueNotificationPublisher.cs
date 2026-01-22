namespace Comanda.Infrastructure.Notifications;

using Comanda.Application.Notifications;

public sealed class QueueNotificationPublisher(INotificationQueue queue) : INotificationPublisher
{
    private readonly INotificationQueue _queue = queue;

    public async Task PublishAsync(
        INotification notification,
        CancellationToken ct = default)
    {
        await _queue.EnqueueAsync(notification);
    }
}







