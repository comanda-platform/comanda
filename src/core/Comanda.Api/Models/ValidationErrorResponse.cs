namespace Comanda.Api.Auth.Models;

public record ValidationErrorResponse(
    string Message,
    Dictionary<string, string[]> Errors);






