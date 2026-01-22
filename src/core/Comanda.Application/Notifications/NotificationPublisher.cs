namespace Comanda.Infrastructure.Notifications;

using Comanda.Application.Notifications;

public sealed class NotificationPublisher : INotificationPublisher
{
    private readonly INotificationQueue _queue;

    public NotificationPublisher(INotificationQueue queue)
    {
        _queue = queue;
    }

    public Task PublishAsync(INotification notification, CancellationToken ct = default)
    {
        return _queue.EnqueueAsync(notification).AsTask();
    }
}







