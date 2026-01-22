namespace Comanda.Infrastructure.Notifications;

using System.Threading.Channels;
using Comanda.Application.Notifications;

public sealed class InMemoryNotificationQueue : INotificationQueue
{
    private readonly Channel<INotification> _channel =
        Channel.CreateUnbounded<INotification>();

    public ValueTask EnqueueAsync(INotification notification)
        => _channel.Writer.WriteAsync(notification);

    public IAsyncEnumerable<INotification> DequeueAsync(CancellationToken ct)
        => _channel.Reader.ReadAllAsync(ct);
}







