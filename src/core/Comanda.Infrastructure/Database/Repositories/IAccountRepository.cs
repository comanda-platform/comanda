namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IAccountRepository : IGenericDatabaseRepository<AccountDatabaseEntity>
{
    Task<AccountDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<AccountDatabaseEntity>> GetWithCreditLineAsync();
}
