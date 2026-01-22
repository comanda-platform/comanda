namespace Comanda.Api.Models;

/// <summary>
/// Query parameters for filtering ledger entries
/// </summary>
public record LedgerEntryQueryParameters
{
    public string? ClientId { get; init; }
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }
}







