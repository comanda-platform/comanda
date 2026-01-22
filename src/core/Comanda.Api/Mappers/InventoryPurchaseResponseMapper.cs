using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class InventoryPurchaseResponseMapper
{
    public static InventoryPurchaseResponse ToResponse(Domain.Entities.InventoryPurchase purchase) 
        => new(
            purchase.PublicId,
            purchase.Supplier.PublicId,
            purchase.Supplier.Name,
            purchase.PurchaseType,
            purchase.PurchasedAt,
            purchase.DeliveredAt,
            purchase.StoreLocationPublicId,
            purchase.Lines.Select(l => new InventoryPurchaseLineResponse(
                l.Item.PublicId,
                l.Item.PublicId,
                l.Item.Name,
                l.Quantity,
                l.UnitPrice,
                l.Unit.PublicId,
                l.Unit.Code)));
}






