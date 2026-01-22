namespace Comanda.Api.Models;

public record CreateAccountRequest(
    string Name,
    bool HasCreditLine = false,
    decimal? CreditLimit = null);

public record UpdateAccountRequest(string Name);

public record UpdateCreditLineRequest(
    bool HasCreditLine,
    decimal? CreditLimit = null);

public record AccountQueryParameters
{
    public bool? WithCreditLine { get; init; }
}

public record PatchAccountRequest
{
    public string? Name { get; init; }
    public bool? HasCreditLine { get; init; }
    public decimal? CreditLimit { get; init; }
}

public record AccountResponse(
    string PublicId,
    string Name,
    bool HasCreditLine,
    decimal? CreditLimit);
