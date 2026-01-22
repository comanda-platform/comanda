namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class SideRepositoryAdapter(
    Database.Repositories.ISideRepository databaseRepository,
    Context context) : Domain.Repositories.ISideRepository
{
    private readonly Database.Repositories.ISideRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Side?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<Side?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Side>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Side>> GetActiveAsync()
    {
        var entities = await _databaseRepository.GetActiveAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(Side side)
    {
        var entity = side.ToPersistence();
        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Side side)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(side.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Side, side.PublicId);

        //side.UpdatePersistence(entity); // TODO: Implement this method if needed

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Side side)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(side.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Side, side.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}







