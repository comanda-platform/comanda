namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

public record CreateInventoryPurchaseRequest(
    string SupplierPublicId,
    InventoryPurchaseType PurchaseType,
    DateTime PurchasedAt,
    string StoreLocationPublicId);

public record AddInventoryPurchaseLineRequest(
    string InventoryItemPublicId,
    decimal Quantity,
    decimal UnitPrice,
    string UnitPublicId);

public record UpdateInventoryPurchaseTypeRequest(
    InventoryPurchaseType PurchaseType);

public record MarkDeliveredRequest(
    DateTime? DeliveredAt = null);

public record InventoryPurchaseResponse(
    string PublicId,
    string SupplierPublicId,
    string SupplierName,
    InventoryPurchaseType PurchaseType,
    DateTime PurchasedAt,
    DateTime? DeliveredAt,
    string StoreLocationPublicId,
    IEnumerable<InventoryPurchaseLineResponse> Lines);

public record InventoryPurchaseLineResponse(
    string PublicId,
    string InventoryItemPublicId,
    string InventoryItemName,
    decimal Quantity,
    decimal UnitPrice,
    string UnitPublicId,
    string UnitCode);







