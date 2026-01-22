namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class PersonMapper
{
    extension(PersonDatabaseEntity dbEntity)
    {
        public Person FromPersistence() =>
            Person.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Name,
                contacts: dbEntity.Contacts?
                    .Select(c => c.FromPersistence())
                    .ToList() ?? []);
    }

    extension(Person domainEntity)
    {
        public PersonDatabaseEntity ToPersistence() =>
            new()
            {
                PublicId = domainEntity.PublicId,
                Name = domainEntity.Name,
                CreatedAt = DateTime.UtcNow
            };

        public void UpdatePersistence(PersonDatabaseEntity dbEntity)
        {
            dbEntity.Name = domainEntity.Name;
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}
