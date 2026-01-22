using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class InventoryItemResponseMapper
{
    public static InventoryItemResponse ToResponse(Domain.Entities.InventoryItem item) 
        => new(
            item.PublicId,
            item.Name,
            item.BaseUnit.PublicId,
            item.BaseUnit.Code,
            item.BaseUnit.Name,
            item.CreatedAt);
}






