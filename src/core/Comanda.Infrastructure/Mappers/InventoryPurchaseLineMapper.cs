namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class InventoryPurchaseLineMapper
{
    extension(InventoryPurchaseLineDatabaseEntity dbEntity)
    {
        public InventoryPurchaseLine FromPersistence() =>
            InventoryPurchaseLine.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Quantity,
                dbEntity.UnitPrice,
                dbEntity.CreatedAt,
                dbEntity.Item.FromPersistence(),
                dbEntity.Unit.FromPersistence(),
                dbEntity.Purchase.PublicId);
    }

    extension(InventoryPurchaseLine domainEntity)
    {
        public InventoryPurchaseLineDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                Quantity = domainEntity.Quantity,
                UnitPrice = domainEntity.UnitPrice,
                CreatedAt = domainEntity.CreatedAt,
                //InventoryPurchaseId = domain.InventoryPurchaseId, // TODO: To be set on the object in infrastructure layer 
                //InventoryItemId = domain.Item.Id, // TODO: To be set on the object in infrastructure layer 
                //UnitId = domain.Unit.Id // TODO: To be set on the object in infrastructure layer 
            };

        public void UpdatePersistence(InventoryPurchaseLineDatabaseEntity dbEntity)
        {
            dbEntity.Quantity = domainEntity.Quantity;
            dbEntity.UnitPrice = domainEntity.UnitPrice;
            //entity.InventoryItemId = domain.Item.Id; // TODO: To be set on the object in infrastructure layer 
            //entity.UnitId = domain.Unit.Id; // TODO: To be set on the object in infrastructure layer 
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}







