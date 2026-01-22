namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class AccountRepositoryAdapter(
    Database.Repositories.IAccountRepository databaseRepository,
    Context context) : Domain.Repositories.IAccountRepository
{
    private readonly Database.Repositories.IAccountRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Account?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);
        return entity?.FromPersistence();
    }

    public async Task<Account?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);
        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Account>> GetWithCreditLineAsync()
    {
        var entities = await _databaseRepository.GetWithCreditLineAsync();
        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(Account account)
    {
        var entity = account.ToPersistence();
        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Account account)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(account.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Account, account.PublicId);

        account.UpdatePersistence(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Account account)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(account.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Account, account.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}
