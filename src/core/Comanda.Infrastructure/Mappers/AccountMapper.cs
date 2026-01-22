namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class AccountMapper
{
    extension(AccountDatabaseEntity dbEntity)
    {
        public Account FromPersistence() =>
            Account.Rehydrate(
                dbEntity.PublicId,
                dbEntity.Name,
                dbEntity.HasCreditLine,
                dbEntity.CreditLimit,
                locations: dbEntity.Locations?
                    .Select(l => l.FromPersistence())
                    .ToList() ?? []);
    }

    extension(Account domainEntity)
    {
        public AccountDatabaseEntity ToPersistence() =>
            new()
            {
                PublicId = domainEntity.PublicId,
                Name = domainEntity.Name,
                HasCreditLine = domainEntity.HasCreditLine,
                CreditLimit = domainEntity.CreditLimit,
                CreatedAt = DateTime.UtcNow
            };

        public void UpdatePersistence(AccountDatabaseEntity dbEntity)
        {
            dbEntity.Name = domainEntity.Name;
            dbEntity.HasCreditLine = domainEntity.HasCreditLine;
            dbEntity.CreditLimit = domainEntity.CreditLimit;
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}
