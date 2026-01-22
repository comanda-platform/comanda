namespace Comanda.Api.Models;

/// <summary>
/// Query parameters for filtering production batches
/// </summary>
public record ProductionBatchQueryParameters
{
    public DateOnly? Date { get; init; }
    public string? ProductId { get; init; }
    public string? DailyMenuId { get; init; }
    public bool? InProgress { get; init; }
}







