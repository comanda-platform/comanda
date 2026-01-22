namespace Comanda.Api.Notifications;

using Microsoft.AspNetCore.SignalR;
using Comanda.Api.SignalR;
using Comanda.Application.Notifications;

public class SignalRNotificationDispatcher(IHubContext<AppHub> hub) : INotificationDispatcher
{
    private readonly IHubContext<AppHub> _hub = hub;

    public Task DispatchAsync(INotification notification, CancellationToken ct = default) 
        => _hub.Clients.All.SendAsync(
            notification.Name,
            notification.Payload,
            ct);
}







