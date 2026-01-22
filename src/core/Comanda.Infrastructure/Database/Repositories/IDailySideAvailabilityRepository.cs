namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface IDailySideAvailabilityRepository : IGenericDatabaseRepository<DailySideAvailabilityDatabaseEntity>
{
    Task<DailySideAvailabilityDatabaseEntity?> GetByPublicIdAsync(string publicId);
    //Task<DailySideAvailabilityDatabaseEntity?> GetBySideIdAndDateAsync(int sideId, DateOnly date);
    //Task<DailySideAvailabilityDatabaseEntity?> GetBySidePublicIdAndDateAsync(string sidePublicId, DateOnly date);
    Task<IEnumerable<DailySideAvailabilityDatabaseEntity>> GetByDateAsync(DateOnly date);
    //Task<IEnumerable<DailySideAvailabilityDatabaseEntity>> GetBySideIdAsync(int sideId);
    //Task<IEnumerable<DailySideAvailabilityDatabaseEntity>> GetBySidePublicIdAsync(string sidePublicId);
    Task<IEnumerable<DailySideAvailabilityDatabaseEntity>> GetAvailableByDateAsync(DateOnly date);
}







