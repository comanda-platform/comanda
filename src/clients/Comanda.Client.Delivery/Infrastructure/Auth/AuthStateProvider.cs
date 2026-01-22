namespace Comanda.Client.Delivery.Infrastructure.Auth;

using Comanda.Client.Delivery.Infrastructure.Services;

public class AuthStateProvider(ISecureStorageService secureStorage)
{
    private const string TokenKey = "auth_token";
    private const string ApiKeyKey = "auth_apikey";
    private const string UsernameKey = "auth_username";
    private const string EmailKey = "auth_email";

    private readonly ISecureStorageService _secureStorage = secureStorage;

    private string? _cachedToken;
    private string? _cachedApiKey;
    private string? _cachedUsername;
    private string? _cachedEmail;
    private bool _isInitialized;

    public event Action? OnAuthStateChanged;

    public async Task InitializeAsync()
    {
        if (_isInitialized) return;

        _cachedToken = await _secureStorage.GetAsync(TokenKey);
        _cachedApiKey = await _secureStorage.GetAsync(ApiKeyKey);
        _cachedUsername = await _secureStorage.GetAsync(UsernameKey);
        _cachedEmail = await _secureStorage.GetAsync(EmailKey);
        _isInitialized = true;
    }

    public async Task<string?> GetTokenAsync()
    {
        await InitializeAsync();

        return _cachedToken;
    }

    public async Task<string?> GetApiKeyAsync()
    {
        await InitializeAsync();

        return _cachedApiKey;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        await InitializeAsync();

        return !string.IsNullOrEmpty(_cachedToken);
    }

    public async Task<string?> GetUsernameAsync()
    {
        await InitializeAsync();

        return _cachedUsername;
    }

    public async Task<string?> GetEmailAsync()
    {
        await InitializeAsync();

        return _cachedEmail;
    }

    public async Task SetAuthStateAsync(
        string token,
        string? apiKey,
        string username,
        string email)
    {
        _cachedToken = token;
        _cachedApiKey = apiKey;
        _cachedUsername = username;
        _cachedEmail = email;

        await _secureStorage.SetAsync(TokenKey, token);

        if (!string.IsNullOrEmpty(apiKey))
            await _secureStorage.SetAsync(ApiKeyKey, apiKey);

        await _secureStorage.SetAsync(UsernameKey, username);
        await _secureStorage.SetAsync(EmailKey, email);

        OnAuthStateChanged?.Invoke();
    }

    public async Task ClearAuthStateAsync()
    {
        _cachedToken = null;
        _cachedApiKey = null;
        _cachedUsername = null;
        _cachedEmail = null;

        await _secureStorage.ClearAllAsync();

        OnAuthStateChanged?.Invoke();
    }
}







