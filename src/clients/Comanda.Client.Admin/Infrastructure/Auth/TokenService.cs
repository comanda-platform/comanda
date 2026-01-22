using Microsoft.JSInterop;

namespace Comanda.Client.Admin.Infrastructure.Auth;

public class TokenService(IJSRuntime jsRuntime)
{
    private const string TokenKey = "authToken";
    private const string ApiKeyKey = "apiKey";
    private readonly IJSRuntime _jsRuntime = jsRuntime;

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);
        }
        catch (InvalidOperationException)
        {
            // During prerendering, JS interop is not available
            return null;
        }
    }

    public async Task SetTokenAsync(string token)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
        }
        catch (InvalidOperationException)
        {
            // During prerendering, JS interop is not available - ignore
        }
    }

    public async Task RemoveTokenAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }
        catch (InvalidOperationException)
        {
            // During prerendering, JS interop is not available - ignore
        }
    }

    public async Task<string?> GetApiKeyAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", ApiKeyKey);
        }
        catch (InvalidOperationException)
        {
            // During prerendering, JS interop is not available
            return null;
        }
    }

    public async Task SetApiKeyAsync(string apiKey)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", ApiKeyKey, apiKey);
        }
        catch (InvalidOperationException)
        {
            // During prerendering, JS interop is not available - ignore
        }
    }

    public async Task RemoveApiKeyAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", ApiKeyKey);
        }
        catch (InvalidOperationException)
        {
            // During prerendering, JS interop is not available - ignore
        }
    }

    public async Task ClearAllAsync()
    {
        await RemoveTokenAsync();
        await RemoveApiKeyAsync();
    }
}










