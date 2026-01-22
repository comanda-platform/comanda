namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface ILedgerEntryRepository : IGenericDatabaseRepository<ClientLedgerEntryDatabaseEntity>
{
    Task<ClientLedgerEntryDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<ClientLedgerEntryDatabaseEntity>> GetByClientIdAsync(int clientId);
    Task<IEnumerable<ClientLedgerEntryDatabaseEntity>> GetByTypeAsync(int ledgerEntryTypeId);
    Task<IEnumerable<ClientLedgerEntryDatabaseEntity>> GetByClientIdAndDateRangeAsync(int clientId, DateTime from, DateTime to);
    Task<decimal> GetClientBalanceAsync(int clientId);
}







