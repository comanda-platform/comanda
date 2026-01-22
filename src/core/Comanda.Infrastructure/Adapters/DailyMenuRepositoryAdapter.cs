namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class DailyMenuRepositoryAdapter(
    Database.Repositories.IDailyMenuRepository databaseRepository,
    Context context) : Domain.Repositories.IDailyMenuRepository
{
    private readonly Database.Repositories.IDailyMenuRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<DailyMenu?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<DailyMenu?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    // Domain interface requires support for either numeric locationId or locationPublicId
    public async Task<DailyMenu?> GetByDateAsync(
        DateOnly date,
        int? locationId = null,
        string? locationPublicId = null)
    {
        if (!string.IsNullOrEmpty(locationPublicId))
        {
            var location = await _context.Locations.FirstOrDefaultAsync(l => l.PublicId == locationPublicId);

            var locId = location?.Id;
            var entity = await _databaseRepository.GetByDateAsync(date, locId);

            return entity?.FromPersistence();
        }

        var dbEntity = await _databaseRepository.GetByDateAsync(
            date,
            locationId,
            locationPublicId);

        return dbEntity?.FromPersistence();
    }

    public async Task<IEnumerable<DailyMenu>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<DailyMenu>> GetByDateRangeAsync(
        DateOnly from,
        DateOnly to,
        int? locationId = null,
        string? locationPublicId = null)
    {
        if (!string.IsNullOrEmpty(locationPublicId))
        {
            // Resolve location public id to numeric id if possible and use numeric-based query
            var location = await _context.Locations.FirstOrDefaultAsync(l => l.PublicId == locationPublicId);
            var locId = location?.Id;

            var entities = await _databaseRepository.GetByDateRangeAsync(
                from,
                to,
                locId);

            return entities.Select(e => e.FromPersistence());
        }

        var list = await _databaseRepository.GetByDateRangeAsync(
            from,
            to,
            locationId);

        return list.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<DailyMenu>> GetByLocationIdAsync(int locationId)
    {
        var entities = await _databaseRepository.GetByLocationIdAsync(locationId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<DailyMenu>> GetByLocationPublicIdAsync(string locationPublicId)
    {
        var location = await _context.Locations.FirstOrDefaultAsync(l => l.PublicId == locationPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Location, locationPublicId);

        var entities = await _databaseRepository.GetByLocationIdAsync(location.Id);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(DailyMenu menu)
    {
        var entity = menu.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(DailyMenu menu)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(menu.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.DailyMenu, menu.PublicId);

        menu.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(DailyMenu menu)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(menu.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.DailyMenu, menu.PublicId);

        await _databaseRepository.DeleteAsync(entity);
    }
}







