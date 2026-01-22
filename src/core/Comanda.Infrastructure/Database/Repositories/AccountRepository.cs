namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class AccountRepository(Context context)
    : GenericDatabaseRepository<AccountDatabaseEntity>(context), IAccountRepository
{
    public override async Task<AccountDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query()
            .FirstOrDefaultAsync(a => a.PublicId == publicId);

    public async Task<IEnumerable<AccountDatabaseEntity>> GetWithCreditLineAsync() =>
        await Query()
            .Where(a => a.HasCreditLine)
            .ToListAsync();
}
