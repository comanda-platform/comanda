namespace Comanda.Api.Models;

/// <summary>
/// Request model for patching a side
/// </summary>
public record PatchSideRequest
{
    public string? Name { get; init; }
    public bool? IsActive { get; init; }
}

/// <summary>
/// Query parameters for filtering sides
/// </summary>
public record SideQueryParameters
{
    public bool? Active { get; init; }
}







