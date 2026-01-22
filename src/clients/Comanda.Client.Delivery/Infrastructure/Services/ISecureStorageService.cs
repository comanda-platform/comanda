namespace Comanda.Client.Delivery.Infrastructure.Services;

public interface ISecureStorageService
{
    Task<string?> GetAsync(string key);
    
    Task SetAsync(
        string key,
        string value);
    
    Task RemoveAsync(string key);

    Task ClearAllAsync();
}







