namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class AuthorizationRepositoryAdapter(
    Database.Repositories.IAuthorizationRepository databaseRepository,
    Database.Repositories.IPersonRepository personRepository,
    Database.Repositories.IAccountRepository accountRepository,
    Context context) : Domain.Repositories.IAuthorizationRepository
{
    private readonly Database.Repositories.IAuthorizationRepository _databaseRepository = databaseRepository;
    private readonly Database.Repositories.IPersonRepository _personRepository = personRepository;
    private readonly Database.Repositories.IAccountRepository _accountRepository = accountRepository;
    private readonly Context _context = context;

    public async Task<Authorization?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);
        return entity?.FromPersistence();
    }

    public async Task<Authorization?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);
        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Authorization>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Authorization>> GetByPersonPublicIdAsync(string personPublicId)
    {
        var person = await _personRepository.GetByPublicIdAsync(personPublicId);
        if (person == null) return [];

        var entities = await _databaseRepository.GetByPersonIdAsync(person.Id);
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Authorization>> GetByAccountPublicIdAsync(string accountPublicId)
    {
        var account = await _accountRepository.GetByPublicIdAsync(accountPublicId);
        if (account == null) return [];

        var entities = await _databaseRepository.GetByAccountIdAsync(account.Id);
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Authorization>> GetActiveByPersonPublicIdAsync(string personPublicId)
    {
        var person = await _personRepository.GetByPublicIdAsync(personPublicId);
        if (person == null) return [];

        var entities = await _databaseRepository.GetActiveByPersonIdAsync(person.Id);
        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Authorization>> GetActiveByAccountPublicIdAsync(string accountPublicId)
    {
        var account = await _accountRepository.GetByPublicIdAsync(accountPublicId);
        if (account == null) return [];

        var entities = await _databaseRepository.GetActiveByAccountIdAsync(account.Id);
        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(Authorization authorization)
    {
        var person = await _personRepository.GetByPublicIdAsync(authorization.PersonPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Person, authorization.PersonPublicId);

        var account = await _accountRepository.GetByPublicIdAsync(authorization.AccountPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Account, authorization.AccountPublicId);

        var entity = authorization.ToPersistence(person, account);
        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Authorization authorization)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(authorization.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Authorization, authorization.PublicId);

        authorization.UpdatePersistence(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Authorization authorization)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(authorization.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Authorization, authorization.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}
