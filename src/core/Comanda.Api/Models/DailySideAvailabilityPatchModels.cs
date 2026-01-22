namespace Comanda.Api.Models;

/// <summary>
/// Request model for patching daily side availability
/// </summary>
public record PatchDailySideAvailabilityRequest
{
    public bool? IsAvailable { get; init; }
}

/// <summary>
/// Query parameters for filtering daily side availability
/// </summary>
public record DailySideAvailabilityQueryParameters
{
    public DateOnly? Date { get; init; }
    public bool? Available { get; init; }
    public string? SideId { get; init; }
}







