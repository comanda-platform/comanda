using Microsoft.AspNetCore.SignalR.Client;

namespace Comanda.Client.Admin.Infrastructure.Notifications;

public class NotificationHubService : IAsyncDisposable
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<NotificationHubService> _logger;
    private HubConnection? _hubConnection;

    public event Func<string, Task>? OrderCreated;
    public event Func<string, Task>? OrderUpdated;
    public event Func<string, Task>? OrderStatusChanged;
    public event Func<string, Task>? OrderLineUpdated;

    public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

    public NotificationHubService(IConfiguration configuration, ILogger<NotificationHubService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task StartAsync(string token, string apiKey)
    {
        if (_hubConnection != null)
        {
            _logger.LogWarning("SignalR connection already exists");
            return;
        }

        try
        {
            var hubUrl = _configuration["ApiSettings:BaseUrl"]?.TrimEnd('/') + "/hubs/notifications";

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult<string?>(token);
                    options.Headers["X-Api-Key"] = apiKey;
                })
                .WithAutomaticReconnect()
                .Build();

            // Configure timeouts
            _hubConnection.ServerTimeout = TimeSpan.FromSeconds(30);
            _hubConnection.HandshakeTimeout = TimeSpan.FromSeconds(15);

            // Register event handlers
            _hubConnection.On<string>("OrderCreated", async (orderPublicId) =>
            {
                _logger.LogInformation("Order created: {OrderId}", orderPublicId);
                if (OrderCreated != null)
                    await OrderCreated.Invoke(orderPublicId);
            });

            _hubConnection.On<string>("OrderUpdated", async (orderPublicId) =>
            {
                _logger.LogInformation("Order updated: {OrderId}", orderPublicId);
                if (OrderUpdated != null)
                    await OrderUpdated.Invoke(orderPublicId);
            });

            _hubConnection.On<string>("OrderStatusChanged", async (orderPublicId) =>
            {
                _logger.LogInformation("Order status changed: {OrderId}", orderPublicId);
                if (OrderStatusChanged != null)
                    await OrderStatusChanged.Invoke(orderPublicId);
            });

            _hubConnection.On<string>("OrderLineUpdated", async (orderLinePublicId) =>
            {
                _logger.LogInformation("Order line updated: {LineId}", orderLinePublicId);
                if (OrderLineUpdated != null)
                    await OrderLineUpdated.Invoke(orderLinePublicId);
            });

            await _hubConnection.StartAsync();
            _logger.LogInformation("SignalR connection established");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start SignalR connection");
            throw;
        }
    }

    public async Task StopAsync()
    {
        if (_hubConnection != null)
        {
            try
            {
                await _hubConnection.StopAsync();
                _logger.LogInformation("SignalR connection stopped");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error stopping SignalR connection");
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection != null)
        {
            await _hubConnection.DisposeAsync();
            _hubConnection = null;
        }
    }
}










