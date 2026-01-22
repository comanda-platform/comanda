namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class InventoryItemMapper
{
    extension(InventoryItemDatabaseEntity dbEntity)
    {
        public InventoryItem FromPersistence() => 
            InventoryItem.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Name,
                dbEntity.CreatedAt,
                dbEntity.BaseUnit.FromPersistence());
    }

    extension(InventoryItem domainEntity)
    {
        public InventoryItemDatabaseEntity ToPersistence() =>
            new()
            {
                PublicId = domainEntity.PublicId,
                Name = domainEntity.Name,
                CreatedAt = domainEntity.CreatedAt,
                //BaseUnitId = domain.BaseUnit.Id, // TODO: To be set on the object in infrastructure layer 
                //BaseUnit = domain.BaseUnit.ToPersistence() // TODO: To be set on the object in infrastructure layer 
            };

        public void UpdatePersistence(InventoryItemDatabaseEntity dbEntity)
        {
            dbEntity.Name = domainEntity.Name;
            // entity.BaseUnitId = domain.BaseUnit.Id; // TODO: To be set on the object in infrastructure layer 
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}







