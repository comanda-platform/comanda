namespace Comanda.Client.Delivery.Infrastructure.Services;

public class SecureStorageService : ISecureStorageService
{
    public async Task<string?> GetAsync(string key)
    {
        try
        {
            return await SecureStorage.Default.GetAsync(key);
        }
        catch (Exception)
        {
            // Handle case where SecureStorage is unavailable
            return null;
        }
    }

    public async Task SetAsync(
        string key,
        string value)
    {
        await SecureStorage.Default.SetAsync(
            key,
            value);
    }

    public Task RemoveAsync(string key)
    {
        SecureStorage.Default.Remove(key);

        return Task.CompletedTask;
    }

    public Task ClearAllAsync()
    {
        SecureStorage.Default.RemoveAll();

        return Task.CompletedTask;
    }
}







