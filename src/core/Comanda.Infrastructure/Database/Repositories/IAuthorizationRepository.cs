namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IAuthorizationRepository : IGenericDatabaseRepository<AuthorizationDatabaseEntity>
{
    Task<AuthorizationDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<AuthorizationDatabaseEntity>> GetByPersonIdAsync(int personId);
    Task<IEnumerable<AuthorizationDatabaseEntity>> GetByAccountIdAsync(int accountId);
    Task<IEnumerable<AuthorizationDatabaseEntity>> GetActiveByPersonIdAsync(int personId);
    Task<IEnumerable<AuthorizationDatabaseEntity>> GetActiveByAccountIdAsync(int accountId);
}
