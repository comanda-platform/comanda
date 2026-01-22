namespace Comanda.Client.Admin.Models;

public record LoginRequest(string Email, string Password);

public record AuthResponse(string Token, string PublicId, string UserName, string Email);










