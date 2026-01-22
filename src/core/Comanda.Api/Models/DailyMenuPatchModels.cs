namespace Comanda.Api.Models;

/// <summary>
/// Query parameters for filtering daily menus
/// </summary>
public record DailyMenuQueryParameters
{
    public DateOnly? Date { get; init; }
    public DateOnly? From { get; init; }
    public DateOnly? To { get; init; }
    public string? LocationId { get; init; }
}







