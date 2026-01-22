namespace Comanda.Client.Delivery.Infrastructure.ApiClients;

using Comanda.Client.Delivery.Infrastructure.ApiClients.ApiModels;

public interface IDeliveryApiClient
{
    // Auth
    Task<AuthResponse?> LoginAsync(string email, string password);

    // Orders
    Task<IEnumerable<OrderResponse>> GetOrdersReadyForDeliveryAsync();
    Task<OrderResponse?> GetOrderAsync(string publicId);
    Task<bool> StartDeliveryAsync(string publicId);
    Task<bool> MarkDeliveredAsync(string publicId);

    // Locations
    Task<LocationResponse?> GetLocationAsync(string publicId);
}







