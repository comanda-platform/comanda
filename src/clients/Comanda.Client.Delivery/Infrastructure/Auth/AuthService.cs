namespace Comanda.Client.Delivery.Infrastructure.Auth;

using Comanda.Client.Delivery.Infrastructure.ApiClients;

public class AuthService : IAuthService
{
    private readonly IDeliveryApiClient _apiClient;
    private readonly AuthStateProvider _authStateProvider;

    public AuthService(
        IDeliveryApiClient apiClient,
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







