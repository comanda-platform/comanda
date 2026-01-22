namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

/// <summary>
/// Request model for patching an inventory purchase
/// </summary>
public record PatchInventoryPurchaseRequest
{
    public InventoryPurchaseType? PurchaseType { get; init; }
    public DateTime? DeliveredAt { get; init; }
}

/// <summary>
/// Query parameters for filtering inventory purchases
/// </summary>
public record InventoryPurchaseQueryParameters
{
    public bool? PendingDelivery { get; init; }
    public InventoryPurchaseType? Type { get; init; }
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }
    public string? SupplierId { get; init; }
}







