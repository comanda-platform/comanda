namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class ClientGroupRepositoryAdapter(
    Database.Repositories.IClientGroupRepository databaseRepository,
    Context context) : Domain.Repositories.IClientGroupRepository
{
    private readonly Database.Repositories.IClientGroupRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<ClientGroup?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<ClientGroup?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<ClientGroup>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<ClientGroup>> GetWithCreditLineAsync()
    {
        var entities = await _databaseRepository.GetWithCreditLineAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(ClientGroup clientGroup)
    {
        var entity = clientGroup.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(ClientGroup clientGroup)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(clientGroup.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.ClientGroup, clientGroup.PublicId);

        clientGroup.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ClientGroup clientGroup)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(clientGroup.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.ClientGroup, clientGroup.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}







