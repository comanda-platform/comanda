using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;
using Comanda.Client.Delivery.Infrastructure.Auth;
using Comanda.Client.Delivery.Infrastructure.Services;
using Comanda.Client.Delivery.Infrastructure.ApiClients;
using Comanda.Client.Delivery.Infrastructure.Localization;
using Comanda.Client.Delivery.Infrastructure.Notifications;

namespace Comanda.Client.Delivery;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddMauiBlazorWebView();

        // MudBlazor Services
        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = true;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 3000;
        });

        // Infrastructure Services
        builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();
        builder.Services.AddSingleton<LocalizationService>();
        builder.Services.AddSingleton<AuthStateProvider>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        // API Clients
        builder.Services.AddHttpClient<IDeliveryApiClient, DeliveryApiClient>(client =>
        {
            client.BaseAddress = new Uri(GetApiBaseUrl());
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        // Application State Services
        builder.Services.AddSingleton<DeliveryStateService>();
        
        // Platform Services
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);

        // SignalR Notification Services
        builder.Services.AddSingleton(sp =>
        {
            var authStateProvider = sp.GetRequiredService<AuthStateProvider>();
            return new NotificationHubService(authStateProvider, GetHubUrl());
        });
        builder.Services.AddSingleton<OrderNotificationHandler>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static string GetApiBaseUrl()
    {
#if DEBUG
        // Development API URL
        return DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:7002"  // Android emulator localhost
            : "http://localhost:7002";
#else
        return "https://api.Comanda.com";
#endif
    }

    private static string GetHubUrl()
    {
#if DEBUG
        // Development SignalR Hub URL
        return DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:7002/hub"  // Android emulator localhost
            : "http://localhost:7002/hub";
#else
        return "https://api.Comanda.com/hub";
#endif
    }
}










