namespace Comanda.Api.Models;

/// <summary>
/// Query parameters for filtering notes
/// </summary>
public record NoteQueryParameters
{
    public string? ClientId { get; init; }
    public string? ClientGroupId { get; init; }
    public string? LocationId { get; init; }
    public string? OrderId { get; init; }
    public string? OrderLineId { get; init; }
    public string? ProductId { get; init; }
    public string? SideId { get; init; }
}







