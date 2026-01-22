namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public interface ILedgerEntryRepository
{
    Task<LedgerEntry?> GetByIdAsync(int id);
    Task<LedgerEntry?> GetByPublicIdAsync(string publicId);

    // DB numeric methods (kept for database adapters)
    Task<IEnumerable<LedgerEntry>> GetByClientIdAsync(int clientId);
    Task<IEnumerable<LedgerEntry>> GetByTypeAsync(LedgerEntryType type);
    Task<IEnumerable<LedgerEntry>> GetByClientIdAndDateRangeAsync(int clientId, DateTime from, DateTime to);
    Task<decimal> GetClientBalanceAsync(int clientId);

    // Domain public-id methods
    Task<IEnumerable<LedgerEntry>> GetByClientPublicIdAsync(string clientPublicId);
    Task<IEnumerable<LedgerEntry>> GetByClientPublicIdAndDateRangeAsync(string clientPublicId, DateTime from, DateTime to);
    Task<decimal> GetClientBalanceByPublicIdAsync(string clientPublicId);

    Task AddAsync(LedgerEntry entry);
}







