namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class SideMapper
{
    extension(SideDatabaseEntity dbEntity)
    {
        public Side FromPersistence() => 
            Side.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Name,
                dbEntity.IsActive,
                dbEntity.CreatedAt);
    }

    extension(Side domainEntity)
    {
        public SideDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                Name = domainEntity.Name,
                IsActive = domainEntity.IsActive,
                CreatedAt = domainEntity.CreatedAt
            };

        public void ApplyToPersistence(SideDatabaseEntity dbEntity)
        {
            dbEntity.Name = domainEntity.Name;
            dbEntity.IsActive = domainEntity.IsActive;
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}







