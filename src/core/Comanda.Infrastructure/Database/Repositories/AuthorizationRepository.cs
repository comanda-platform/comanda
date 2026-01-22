namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class AuthorizationRepository(Context context)
    : GenericDatabaseRepository<AuthorizationDatabaseEntity>(context), IAuthorizationRepository
{
    public override async Task<AuthorizationDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query()
            .FirstOrDefaultAsync(a => a.PublicId == publicId);

    public async Task<IEnumerable<AuthorizationDatabaseEntity>> GetByPersonIdAsync(int personId) =>
        await Query()
            .Where(a => a.PersonId == personId)
            .ToListAsync();

    public async Task<IEnumerable<AuthorizationDatabaseEntity>> GetByAccountIdAsync(int accountId) =>
        await Query()
            .Where(a => a.AccountId == accountId)
            .ToListAsync();

    public async Task<IEnumerable<AuthorizationDatabaseEntity>> GetActiveByPersonIdAsync(int personId) =>
        await Query()
            .Where(a => a.PersonId == personId && a.IsActive)
            .ToListAsync();

    public async Task<IEnumerable<AuthorizationDatabaseEntity>> GetActiveByAccountIdAsync(int accountId) =>
        await Query()
            .Where(a => a.AccountId == accountId && a.IsActive)
            .ToListAsync();
}
