namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class PersonRepositoryAdapter(
    Database.Repositories.IPersonRepository databaseRepository,
    Context context) : Domain.Repositories.IPersonRepository
{
    private readonly Database.Repositories.IPersonRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Person?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);
        return entity?.FromPersistence();
    }

    public async Task<Person?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);
        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();
        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(Person person)
    {
        var entity = person.ToPersistence();
        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Person person)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(person.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Person, person.PublicId);

        person.UpdatePersistence(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Person person)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(person.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Person, person.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}
