namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class ClientGroupMapper
{
    extension(ClientGroupDatabaseEntity dbEntity)
    {
        public ClientGroup FromPersistence() =>
            ClientGroup.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Name,
                dbEntity.HasCreditLine,

                members: dbEntity.Members?
                    .Select(m => m.FromPersistence())
                    .ToList() ?? [],

                locations: dbEntity.Locations?
                    .Select(l => l.FromPersistence())
                    .ToList() ?? []);
    }

    extension(ClientGroup domainEntity)
    {
        public ClientGroupDatabaseEntity ToPersistence() =>
            new()
            {
                PublicId = domainEntity.PublicId,
                Name = domainEntity.Name,
                HasCreditLine = domainEntity.HasCreditLine,
                CreatedAt = DateTime.UtcNow
            };

        public void UpdatePersistence(ClientGroupDatabaseEntity dbEntity)
        {
            dbEntity.Name = domainEntity.Name;
            dbEntity.HasCreditLine = domainEntity.HasCreditLine;
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}







