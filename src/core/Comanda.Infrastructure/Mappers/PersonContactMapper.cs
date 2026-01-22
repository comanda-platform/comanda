namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public static class PersonContactMapper
{
    extension(PersonContactDatabaseEntity dbEntity)
    {
        public PersonContact FromPersistence() =>
            PersonContact.Rehydrate(
                dbEntity.PublicId,
                (ClientContactType)dbEntity.Type,
                dbEntity.Value);
    }

    extension(PersonContact domainEntity)
    {
        public PersonContactDatabaseEntity ToPersistence(PersonDatabaseEntity personDbEntity) =>
            new()
            {
                PublicId = domainEntity.PublicId,
                PersonId = personDbEntity.Id,
                Person = personDbEntity,
                Type = (int)domainEntity.Type,
                Value = domainEntity.Value,
                CreatedAt = DateTime.UtcNow
            };
    }
}
