namespace Comanda.Client.Admin.Models;

public record AccountResponse(
    string PublicId,
    string Name,
    bool HasCreditLine,
    decimal? CreditLimit);

public record CreateAccountRequest(
    string Name,
    bool HasCreditLine = false,
    decimal? CreditLimit = null);

public record PatchAccountRequest
{
    public string? Name { get; init; }
    public bool? HasCreditLine { get; init; }
    public decimal? CreditLimit { get; init; }
}
