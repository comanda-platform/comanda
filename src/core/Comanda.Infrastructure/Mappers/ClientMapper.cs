namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class ClientMapper
{
    extension(ClientDatabaseEntity dbEntity)
    {
        public Client FromPersistence() => 
            Client.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Name,
                dbEntity.Group?.PublicId,

                contacts: dbEntity.Contacts?
                    .Select(c => c.FromPersistence())
                    .ToList() ?? [],

                locations: dbEntity.Locations?
                    .Select(l => l.FromPersistence())
                    .ToList() ?? []);
    }

    extension(Client domainEntity)
    {
        public ClientDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                Name = domainEntity.Name,
                //ClientGroupId = domain.ClientGroupId, // TODO: To be set on the object in infrastructure layer 
                CreatedAt = DateTime.UtcNow
            };

        public void UpdatePersistence(ClientDatabaseEntity dbEntity)
        {
            dbEntity.Name = domainEntity.Name;
            //entity.ClientGroupId = domain.ClientGroupId; // TODO: To be set on the object in infrastructure layer? not sure.
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}







