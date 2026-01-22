namespace Comanda.Infrastructure.Adapters;

using Comanda.Database;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class NoteRepositoryAdapter(
    Database.Repositories.INoteRepository databaseRepository,
    Context context) : Domain.Repositories.INoteRepository
{
    private readonly Database.Repositories.INoteRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Note?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<Note?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Note>> GetByClientPublicIdAsync(string clientPublicId)
    {
        var entities = await _databaseRepository.GetByClientPublicIdAsync(clientPublicId);

        return entities
            .Select(e => e.FromPersistence())
            .ToList();
    }

    public async Task<IEnumerable<Note>> GetByClientGroupPublicIdAsync(string clientGroupPublicId)
    {
        var entities = await _databaseRepository.GetByClientGroupPublicIdAsync(clientGroupPublicId);

        return entities
            .Select(e => e.FromPersistence())
            .ToList();
    }

    public async Task<IEnumerable<Note>> GetByLocationPublicIdAsync(string locationPublicId)
    {
        var entities = await _databaseRepository.GetByLocationPublicIdAsync(locationPublicId);

        return entities
            .Select(e => e.FromPersistence())
            .ToList();
    }

    public async Task<IEnumerable<Note>> GetByOrderPublicIdAsync(string orderPublicId)
    {
        var entities = await _databaseRepository.GetByOrderPublicIdAsync(orderPublicId);

        return entities
            .Select(e => e.FromPersistence())
            .ToList();
    }

    public async Task<IEnumerable<Note>> GetByOrderLinePublicIdAsync(string orderLinePublicId)
    {
        var entities = await _databaseRepository.GetByOrderLinePublicIdAsync(orderLinePublicId);

        return entities
            .Select(e => e.FromPersistence())
            .ToList();
    }

    public async Task<IEnumerable<Note>> GetByProductPublicIdAsync(string productPublicId)
    {
        var entities = await _databaseRepository.GetByProductPublicIdAsync(productPublicId);

        return entities
            .Select(e => e.FromPersistence())
            .ToList();
    }

    public async Task<IEnumerable<Note>> GetBySidePublicIdAsync(string sidePublicId)
    {
        var entities = await _databaseRepository.GetBySidePublicIdAsync(sidePublicId);

        return entities
            .Select(e => e.FromPersistence())
            .ToList();
    }

    public async Task AddAsync(Note note)
    {
        var entity = note.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task DeleteAsync(Note note)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(note.PublicId)
            ?? throw new InvalidOperationException($"Note '{note.PublicId}' not found");

        await _databaseRepository.DeleteAsync(entity);
    }
}







