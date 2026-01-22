namespace Comanda.Application.Notifications;

public interface INotificationPublisher
{
    Task PublishAsync(INotification notification, CancellationToken ct = default);
}







