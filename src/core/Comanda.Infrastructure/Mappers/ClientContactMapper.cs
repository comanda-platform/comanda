namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public static class ClientContactMapper
{
    extension(ClientContactDatabaseEntity dbEntity)
    {
        public ClientContact FromPersistence() => 
            ClientContact.Rehydrate(
                dbEntity.PublicId,
                (ClientContactType)dbEntity.ContactTypeId,
                dbEntity.Value);
    }

    extension(ClientContact domainEntity)
    {
        public ClientContactDatabaseEntity ToPersistence(ClientDatabaseEntity dbEntity) => 
            new()
            {
                PublicId = domainEntity.PublicId,
                ClientId = dbEntity.Id,
                Client = dbEntity,
                ContactTypeId = (int)domainEntity.Type,
                Value = domainEntity.Value,
                CreatedAt = DateTime.UtcNow
            };
    }
}







