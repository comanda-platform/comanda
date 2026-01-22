namespace Comanda.Client.Kitchen.Infrastructure.Auth;

using Comanda.Client.Kitchen.Infrastructure.ApiClients;

public class AuthService : IAuthService
{
    private readonly IKitchenApiClient _apiClient;
    private readonly AuthStateProvider _authStateProvider;

    public AuthService(
        IKitchenApiClient apiClient,
        AuthStateProvider authStateProvider)
    {
        _apiClient = apiClient;
        _authStateProvider = authStateProvider;
    }

    public async Task<bool> LoginAsync(
        string email,
        string password)
    {
        var response = await _apiClient.LoginAsync(
            email,
            password);

        if (response == null)
            return false;

        await _authStateProvider.SetAuthStateAsync(
            response.Token,
            response.ApiKey,
            response.Username,
            response.Email);

        return true;
    }

    public async Task LogoutAsync()
    {
        await _authStateProvider.ClearAuthStateAsync();
    }

    public async Task<bool> TryAutoLoginAsync()
    {
        return await _authStateProvider.IsAuthenticatedAsync();
    }
}







