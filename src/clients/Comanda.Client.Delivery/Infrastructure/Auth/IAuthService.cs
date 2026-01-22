namespace Comanda.Client.Delivery.Infrastructure.Auth;

public interface IAuthService
{
    Task<bool> LoginAsync(
        string email,
        string password);

    Task LogoutAsync();

    Task<bool> TryAutoLoginAsync();
}







