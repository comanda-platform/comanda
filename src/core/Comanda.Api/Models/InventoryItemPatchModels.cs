namespace Comanda.Api.Models;

/// <summary>
/// Request model for patching an inventory item
/// </summary>
public record PatchInventoryItemRequest
{
    public string? Name { get; init; }
    public string? BaseUnitPublicId { get; init; }
}

/// <summary>
/// Query parameters for filtering inventory items
/// </summary>
public record InventoryItemQueryParameters
{
    public string? SearchTerm { get; init; }
}







