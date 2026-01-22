namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain.Entities;
using Comanda.Infrastructure.Mappers;

public class DailySideAvailabilityRepositoryAdapter(
    Database.Repositories.IDailySideAvailabilityRepository databaseRepository,
    Context context) : Domain.Repositories.IDailySideAvailabilityRepository
{
    private readonly Database.Repositories.IDailySideAvailabilityRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<DailySideAvailability?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<DailySideAvailability?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    //public async Task<DailySideAvailability?> GetBySideIdAndDateAsync(
    //    int sideId, 
    //    DateOnly date)
    //{
    //    var entity = await _databaseRepository.GetBySideIdAndDateAsync(
    //        sideId,
    //        date);

    //    return entity?.FromPersistence();
    //}

    //public async Task<DailySideAvailability?> GetBySidePublicIdAndDateAsync(
    //    string sidePublicId, 
    //    DateOnly date)
    //{
    //    var entity = await _databaseRepository.GetBySidePublicIdAndDateAsync(
    //        sidePublicId,
    //        date);

    //    return entity?.FromPersistence();
    //}

    //public async Task<IEnumerable<DailySideAvailability>> GetBySideIdAndDateAsync(
    //    int sideId,
    //    DateOnly date)
    //{
    //    var entities = await _databaseRepository.GetBySideIdAsync(sideId);

    //    return entities.Select(e => e.FromPersistence());
    //}

    //public async Task<IEnumerable<DailySideAvailability>> GetBySidePublicIdAsync(string sidePublicId)
    //{
    //    var entities = await _databaseRepository.GetBySidePublicIdAsync(sidePublicId);

    //    return entities.Select(e => e.FromPersistence());
    //}

    public async Task<IEnumerable<DailySideAvailability>> GetByDateAsync(DateOnly date)
    {
        var entities = await _databaseRepository.GetByDateAsync(date);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<DailySideAvailability>> GetAvailableByDateAsync(DateOnly date)
    {
        var entities = await _databaseRepository.GetAvailableByDateAsync(date);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task AddAsync(DailySideAvailability availability)
    {
        var entity = availability.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(DailySideAvailability availability)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(availability.PublicId)
            ?? throw new InvalidOperationException($"Daily side availability with PublicId {availability.PublicId} not found");

        availability.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(DailySideAvailability availability)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(availability.PublicId)
            ?? throw new InvalidOperationException($"Daily side availability with PublicId {availability.PublicId} not found");

        await _databaseRepository.DeleteAsync(entity);
    }
}







