namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Infrastructure.Mappers;

public class LedgerEntryRepositoryAdapter(
    Database.Repositories.ILedgerEntryRepository databaseRepository,
    Context context) : Domain.Repositories.ILedgerEntryRepository
{
    private readonly Database.Repositories.ILedgerEntryRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<LedgerEntry?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<LedgerEntry?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<LedgerEntry>> GetByClientIdAsync(int clientId)
    {
        var entities = await _databaseRepository.GetByClientIdAsync(clientId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<LedgerEntry>> GetByTypeAsync(LedgerEntryType type)
    {
        var entities = await _databaseRepository.GetByTypeAsync((int)type);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<LedgerEntry>> GetByClientIdAndDateRangeAsync(
        int clientId,
        DateTime from,
        DateTime to)
    {
        var entities = await _databaseRepository.GetByClientIdAndDateRangeAsync(
            clientId,
            from,
            to);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<decimal> GetClientBalanceAsync(int clientId)
    {
        return await _databaseRepository.GetClientBalanceAsync(clientId);
    }

    // Public-id based methods
    public async Task<IEnumerable<LedgerEntry>> GetByClientPublicIdAsync(string clientPublicId)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.PublicId == clientPublicId)
            ?? throw new InvalidOperationException($"Client '{clientPublicId}' not found");

        var entities = await _databaseRepository.GetByClientIdAsync(client.Id);
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<LedgerEntry>> GetByClientPublicIdAndDateRangeAsync(string clientPublicId, DateTime from, DateTime to)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.PublicId == clientPublicId)
            ?? throw new InvalidOperationException($"Client '{clientPublicId}' not found");

        var entities = await _databaseRepository.GetByClientIdAndDateRangeAsync(client.Id, from, to);
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<decimal> GetClientBalanceByPublicIdAsync(string clientPublicId)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.PublicId == clientPublicId)
            ?? throw new InvalidOperationException($"Client '{clientPublicId}' not found");

        return await _databaseRepository.GetClientBalanceAsync(client.Id);
    }

    public async Task AddAsync(LedgerEntry entry)
    {
        ClientDatabaseEntity? client = null;

        if (!string.IsNullOrEmpty(entry.ClientPublicId))
        {
            client = await _context.Clients.FirstOrDefaultAsync(c => c.PublicId == entry.ClientPublicId);
        }

        if (client is null)
            throw new InvalidOperationException($"Client {entry.ClientPublicId} for ledger entry not found.");

        var entity = entry.ToPersistence(client);
        
        await _databaseRepository.AddAsync(entity);
    }
}







