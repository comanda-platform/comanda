namespace Comanda.Client.Kitchen.Infrastructure.Services;

public interface ISecureStorageService
{
    Task<string?> GetAsync(string key);
    
    Task SetAsync(
        string key,
        string value);
    
    Task RemoveAsync(string key);

    Task ClearAllAsync();
}







