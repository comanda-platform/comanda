using Comanda.Application.Notifications;

namespace Comanda.Api.Notifications;

public class NotificationWorker(
    INotificationQueue queue,
    INotificationDispatcher dispatcher
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var notification in queue.DequeueAsync(stoppingToken))
        {
            await dispatcher.DispatchAsync(notification, stoppingToken);
        }
    }
}







