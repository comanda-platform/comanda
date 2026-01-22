namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

/// <summary>
/// Query parameters for filtering units
/// </summary>
public record UnitQueryParameters
{
    public string? Code { get; init; }
    public UnitCategory? Category { get; init; }
}







