namespace Comanda.Api.Models;

/// <summary>
/// Request model for patching a product
/// </summary>
public record PatchProductRequest
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
}

/// <summary>
/// Query parameters for filtering products
/// </summary>
public record ProductQueryParameters
{
    public string? TypeId { get; init; }
}







