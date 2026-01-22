namespace Comanda.Client.Delivery.Infrastructure.ApiClients.ApiModels;

public record LoginRequest(string Email, string Password);

public record AuthResponse(
    string Token,
    string Email,
    string Username,
    string? ApiKey = null);







