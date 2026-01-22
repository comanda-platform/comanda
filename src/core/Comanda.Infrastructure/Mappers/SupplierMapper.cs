namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public static class SupplierMapper
{
    extension(SupplierDatabaseEntity dbEntity)
    {
        public Supplier FromPersistence() => 
            Supplier.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Name,
                (SupplierType)dbEntity.SupplierTypeId,
                dbEntity.CreatedAt);
    }

    extension(Supplier domainEntity)
    {
        public SupplierDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                Name = domainEntity.Name,
                SupplierTypeId = (int)domainEntity.Type,
                CreatedAt = domainEntity.CreatedAt
            };

        public void UpdatePersistence(SupplierDatabaseEntity dbEntity)
        {
            dbEntity.Name = domainEntity.Name;
            dbEntity.SupplierTypeId = (int)domainEntity.Type;
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}







