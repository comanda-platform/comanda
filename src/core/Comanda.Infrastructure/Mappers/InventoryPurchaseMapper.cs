namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public static class InventoryPurchaseMapper
{
    extension(InventoryPurchaseDatabaseEntity dbEntity)
    {
        public InventoryPurchase FromPersistence() => 
            InventoryPurchase.Rehydrate(
                dbEntity.PublicId,
                dbEntity.PurchasedAt,
                dbEntity.TotalAmount,
                (InventoryPurchaseType)dbEntity.PurchaseTypeId,
                dbEntity.CreatedAt,
                dbEntity.DeliveredAt,
                dbEntity.StoreLocation?.PublicId,

                supplier: dbEntity.Supplier.FromPersistence(),

                lines: dbEntity.Lines
                    .Where(l => !l.IsDeleted)
                    .OrderBy(l => l.Id)
                    .Select(l => l.FromPersistence())
                    .ToList());
    }

    extension(InventoryPurchase domainEntity)
    {
        public InventoryPurchaseDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                PurchasedAt = domainEntity.PurchasedAt,
                TotalAmount = domainEntity.TotalAmount,
                PurchaseTypeId = (int)domainEntity.PurchaseType,
                CreatedAt = domainEntity.CreatedAt,
                DeliveredAt = domainEntity.DeliveredAt,
                //SupplierId = domain.Supplier.Id, // TODO: To be set on the object in infrastructure layer 
                //StoreLocationId = domain.StoreLocationId, // TODO: To be set on the object in infrastructure layer 

                Lines = domainEntity.Lines
                    .Select(l => l.ToPersistence())
                    .ToList()
            };

        public void UpdatePersistence(InventoryPurchaseDatabaseEntity dbEntity)
        {
            dbEntity.PurchasedAt = domainEntity.PurchasedAt;
            dbEntity.TotalAmount = domainEntity.TotalAmount;
            dbEntity.PurchaseTypeId = (int)domainEntity.PurchaseType;
            dbEntity.DeliveredAt = domainEntity.DeliveredAt;
            //entity.StoreLocationId = domain.StoreLocationId; // TODO: To be set on the object in infrastructure layer 
            dbEntity.LastModifiedAt = DateTime.UtcNow;

            // Sync lines // TODO: To be set on the object in infrastructure layer 
            //var domainLineIds = domain.Lines.Select(l => l.Id).ToHashSet();

            //var linesToRemove = entity.Lines
            //    .Where(e => !domainLineIds.Contains(e.Id))
            //    .ToList();

            //foreach (var toRemove in linesToRemove)
            //{
            //    entity.Lines.Remove(toRemove);
            //}

            //foreach (var line in domain.Lines)
            //{
            //    var existing = entity.Lines.FirstOrDefault(e => e.Id == line.Id);

            //    if (existing != null)
            //    {
            //        line.UpdatePersistence(existing);
            //    }
            //    else
            //    {
            //        entity.Lines.Add(line.ToPersistence());
            //    }
            //}
        }
    }
}







