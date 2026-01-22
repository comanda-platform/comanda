namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

/// <summary>
/// Request model for patching a supplier
/// </summary>
public record PatchSupplierRequest
{
    public string? Name { get; init; }
    public SupplierType? Type { get; init; }
}

/// <summary>
/// Query parameters for filtering suppliers
/// </summary>
public record SupplierQueryParameters
{
    public SupplierType? Type { get; init; }
}







