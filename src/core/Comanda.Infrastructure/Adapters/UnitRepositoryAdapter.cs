namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Infrastructure.Mappers;
using Comanda.Domain;

public class UnitRepositoryAdapter(
    Database.Repositories.IUnitRepository databaseRepository,
    Context context) : Domain.Repositories.IUnitRepository
{
    private readonly Database.Repositories.IUnitRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Unit?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<Unit?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<Unit?> GetByCodeAsync(string code)
    {
        var entity = await _databaseRepository.GetByCodeAsync(code);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Unit>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Unit>> GetByCategoryAsync(UnitCategory category)
    {
        var entities = await _databaseRepository.GetByCategoryAsync((int)category);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(Unit unit)
    {
        var entity = unit.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Unit unit)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(unit.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Unit, unit.PublicId);

        unit.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Unit unit)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(unit.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Unit, unit.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}







