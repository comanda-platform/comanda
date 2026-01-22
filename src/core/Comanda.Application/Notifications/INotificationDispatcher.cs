namespace Comanda.Application.Notifications;

public interface INotificationDispatcher
{
    Task DispatchAsync(INotification notification, CancellationToken ct = default);
}







