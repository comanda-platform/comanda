namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public static class UnitMapper
{
    extension(UnitDatabaseEntity dbEntity)
    {
        public Unit FromPersistence() => 
            Unit.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Code,
                dbEntity.Name,
                (UnitCategory)dbEntity.CategoryId,
                dbEntity.ToBaseMultiplier);
    }

    extension(Unit domainEntity)
    {
        public UnitDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                Code = domainEntity.Code,
                Name = domainEntity.Name,
                CategoryId = (int)domainEntity.Category,
                ToBaseMultiplier = domainEntity.ToBaseMultiplier
            };

        public void UpdatePersistence(UnitDatabaseEntity dbEntity)
        {
            dbEntity.Code = domainEntity.Code;
            dbEntity.Name = domainEntity.Name;
            dbEntity.CategoryId = (int)domainEntity.Category;
            dbEntity.ToBaseMultiplier = domainEntity.ToBaseMultiplier;
        }
    }
}







