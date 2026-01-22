namespace Comanda.Client.Kitchen.Infrastructure.Notifications;

using Microsoft.AspNetCore.SignalR.Client;
using Comanda.Client.Kitchen.Infrastructure.Auth;

/// <summary>
/// Service for managing SignalR hub connection and receiving real-time notifications
/// </summary>
public class NotificationHubService : IAsyncDisposable
{
    private readonly AuthStateProvider _authStateProvider;
    private readonly string _hubUrl;
    private HubConnection? _hubConnection;
    private bool _isConnecting;

    /// <summary>
    /// Fired when any notification is received from the hub
    /// </summary>
    public event Action<NotificationEvent>? OnNotificationReceived;

    /// <summary>
    /// Fired when the connection state changes
    /// </summary>
    public event Action<HubConnectionState>? OnConnectionStateChanged;

    /// <summary>
    /// Current connection state
    /// </summary>
    public HubConnectionState ConnectionState =>
        _hubConnection?.State ?? HubConnectionState.Disconnected;

    public NotificationHubService(AuthStateProvider authStateProvider, string hubUrl)
    {
        _authStateProvider = authStateProvider;
        _hubUrl = hubUrl;
    }

    /// <summary>
    /// Start the connection to the notification hub
    /// </summary>
    public async Task StartAsync()
    {
        if (_isConnecting || _hubConnection?.State == HubConnectionState.Connected)
            return;

        _isConnecting = true;
        System.Diagnostics.Debug.WriteLine($"NotificationHubService: Attempting to connect to {_hubUrl}");

        try
        {
            var token = await _authStateProvider.GetTokenAsync();

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl, options =>
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        options.AccessTokenProvider = () => Task.FromResult<string?>(token);
                    }
                })
                .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10) })
                .Build();

            // Set shorter timeouts to prevent hanging
            _hubConnection.ServerTimeout = TimeSpan.FromSeconds(10);
            _hubConnection.HandshakeTimeout = TimeSpan.FromSeconds(5);

            // Register handlers for each order event type
            // The API sends notifications with the event name as the SignalR method name
            RegisterEventHandler(NotificationEventNames.OrderCreated);
            RegisterEventHandler(NotificationEventNames.OrderAccepted);
            RegisterEventHandler(NotificationEventNames.OrderPreparingStarted);
            RegisterEventHandler(NotificationEventNames.OrderReady);
            RegisterEventHandler(NotificationEventNames.OrderDeliveryStarted);
            RegisterEventHandler(NotificationEventNames.OrderDeliveryLocationAssigned);
            RegisterEventHandler(NotificationEventNames.OrderDeliveryFinished);
            RegisterEventHandler(NotificationEventNames.OrderLineCreated);
            RegisterEventHandler(NotificationEventNames.OrderCompleted);
            RegisterEventHandler(NotificationEventNames.OrderCancelled);

            // Handle connection state changes
            _hubConnection.Closed += OnClosed;
            _hubConnection.Reconnecting += OnReconnecting;
            _hubConnection.Reconnected += OnReconnected;

            await _hubConnection.StartAsync();
            System.Diagnostics.Debug.WriteLine("NotificationHubService: Connected successfully!");
            OnConnectionStateChanged?.Invoke(HubConnectionState.Connected);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"NotificationHubService: Failed to connect - {ex.Message}");
            OnConnectionStateChanged?.Invoke(HubConnectionState.Disconnected);
        }
        finally
        {
            _isConnecting = false;
        }
    }

    /// <summary>
    /// Stop the connection to the notification hub
    /// </summary>
    public async Task StopAsync()
    {
        if (_hubConnection is not null)
        {
            _hubConnection.Closed -= OnClosed;
            _hubConnection.Reconnecting -= OnReconnecting;
            _hubConnection.Reconnected -= OnReconnected;

            await _hubConnection.StopAsync();
            await _hubConnection.DisposeAsync();
            _hubConnection = null;

            OnConnectionStateChanged?.Invoke(HubConnectionState.Disconnected);
        }
    }

    /// <summary>
    /// Register a handler for a specific event name
    /// </summary>
    private void RegisterEventHandler(string eventName)
    {
        _hubConnection!.On<object>(eventName, payload =>
        {
            System.Diagnostics.Debug.WriteLine($"NotificationHubService: Raw payload type: {payload?.GetType().Name ?? "null"}");
            System.Diagnostics.Debug.WriteLine($"NotificationHubService: Raw payload: {payload}");
            
            string payloadJson;
            if (payload is string str)
            {
                payloadJson = str;
            }
            else if (payload is System.Text.Json.JsonElement jsonElement)
            {
                payloadJson = jsonElement.GetRawText();
            }
            else
            {
                payloadJson = System.Text.Json.JsonSerializer.Serialize(payload);
            }
            
            System.Diagnostics.Debug.WriteLine($"NotificationHubService: Serialized payload: {payloadJson}");
            HandleNotification(eventName, payloadJson);
        });
    }

    private void HandleNotification(string name, string payload)
    {
        System.Diagnostics.Debug.WriteLine($"NotificationHubService: Received notification - {name}");
        OnNotificationReceived?.Invoke(new NotificationEvent(name, payload));
    }

    private Task OnClosed(Exception? exception)
    {
        System.Diagnostics.Debug.WriteLine($"NotificationHubService: Connection closed - {exception?.Message}");
        OnConnectionStateChanged?.Invoke(HubConnectionState.Disconnected);
        return Task.CompletedTask;
    }

    private Task OnReconnecting(Exception? exception)
    {
        System.Diagnostics.Debug.WriteLine($"NotificationHubService: Reconnecting - {exception?.Message}");
        OnConnectionStateChanged?.Invoke(HubConnectionState.Reconnecting);
        return Task.CompletedTask;
    }

    private Task OnReconnected(string? connectionId)
    {
        System.Diagnostics.Debug.WriteLine($"NotificationHubService: Reconnected - {connectionId}");
        OnConnectionStateChanged?.Invoke(HubConnectionState.Connected);
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        await StopAsync();
        GC.SuppressFinalize(this);
    }
}







