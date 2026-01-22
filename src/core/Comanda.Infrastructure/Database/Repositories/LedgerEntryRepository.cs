namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class LedgerEntryRepository(Context context)
    : GenericDatabaseRepository<ClientLedgerEntryDatabaseEntity>(context), ILedgerEntryRepository
{
    public override async Task<ClientLedgerEntryDatabaseEntity?> GetByPublicIdAsync(string publicId) => 
        await Query().FirstOrDefaultAsync(e => e.PublicId == publicId);

    public async Task<IEnumerable<ClientLedgerEntryDatabaseEntity>> GetByClientIdAsync(int clientId) =>
        await Query()
            .Where(e => e.ClientId == clientId)
            .OrderByDescending(e => e.OccurredAt)
            .ToListAsync();

    public async Task<IEnumerable<ClientLedgerEntryDatabaseEntity>> GetByTypeAsync(int ledgerEntryTypeId) =>
        await Query()
            .Where(e => e.LedgerEntryTypeId == ledgerEntryTypeId)
            .ToListAsync();

    public async Task<IEnumerable<ClientLedgerEntryDatabaseEntity>> GetByClientIdAndDateRangeAsync(
        int clientId,
        DateTime from,
        DateTime to) =>
        await Query()
            .Where(e => e.ClientId == clientId && e.OccurredAt >= from && e.OccurredAt <= to)
            .OrderByDescending(e => e.OccurredAt)
            .ToListAsync();

    public async Task<decimal> GetClientBalanceAsync(int clientId) =>
        await Query()
            .Where(e => e.ClientId == clientId)
            .SumAsync(e => e.Amount);
}







