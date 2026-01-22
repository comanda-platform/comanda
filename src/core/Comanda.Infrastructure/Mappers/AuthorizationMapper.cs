namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public static class AuthorizationMapper
{
    extension(AuthorizationDatabaseEntity dbEntity)
    {
        public Authorization FromPersistence() =>
            Authorization.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Person.PublicId,
                dbEntity.Account.PublicId,
                (AuthorizationRole)dbEntity.Role,
                dbEntity.IsActive,
                dbEntity.CreatedAt);
    }

    extension(Authorization domainEntity)
    {
        public AuthorizationDatabaseEntity ToPersistence(
            PersonDatabaseEntity personDbEntity,
            AccountDatabaseEntity accountDbEntity) =>
            new()
            {
                PublicId = domainEntity.PublicId,
                PersonId = personDbEntity.Id,
                Person = personDbEntity,
                AccountId = accountDbEntity.Id,
                Account = accountDbEntity,
                Role = (int)domainEntity.Role,
                IsActive = domainEntity.IsActive,
                CreatedAt = domainEntity.CreatedAt
            };

        public void UpdatePersistence(AuthorizationDatabaseEntity dbEntity)
        {
            dbEntity.Role = (int)domainEntity.Role;
            dbEntity.IsActive = domainEntity.IsActive;
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}
