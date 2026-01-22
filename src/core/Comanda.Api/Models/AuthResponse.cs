namespace Comanda.Api.Models;

public record AuthResponse(
    string Token,
    string Email,
    string Username,
    string? ApiKey = null
);







