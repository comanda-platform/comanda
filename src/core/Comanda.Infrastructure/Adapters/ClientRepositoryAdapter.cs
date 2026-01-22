namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class ClientRepositoryAdapter(
    Database.Repositories.IClientRepository databaseRepository,
    Context context) : Domain.Repositories.IClientRepository
{
    private readonly Database.Repositories.IClientRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Client?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<Client?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Client>> GetByGroupIdAsync(int clientGroupId)
    {
        var entities = await _databaseRepository.GetByGroupIdAsync(clientGroupId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Client>> GetByGroupPublicIdAsync(string clientGroupPublicId)
    {
        var entities = await _databaseRepository.GetByGroupPublicIdAsync(clientGroupPublicId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(Client client)
    {
        var entity = client.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Client client)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(client.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Client, client.PublicId);

        client.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Client client)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(client.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Client, client.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}







