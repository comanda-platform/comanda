namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;

public interface IDailySideAvailabilityRepository
{
    Task<DailySideAvailability?> GetByIdAsync(int id);
    //Task<IEnumerable<DailySideAvailability>> GetBySideIdAsync(int id);
    //Task<IEnumerable<DailySideAvailability>> GetBySidePublicIdAsync(string publicId);
    Task<DailySideAvailability?> GetByPublicIdAsync(string publicId);
    //Task<DailySideAvailability?> GetBySideIdAndDateAsync(int id, DateOnly date);
    //Task<DailySideAvailability?> GetBySidePublicIdAndDateAsync(string publicId, DateOnly date);
    Task<IEnumerable<DailySideAvailability>> GetByDateAsync(DateOnly date);
    Task<IEnumerable<DailySideAvailability>> GetAvailableByDateAsync(DateOnly date);
    Task AddAsync(DailySideAvailability availability);
    Task UpdateAsync(DailySideAvailability availability);
    Task DeleteAsync(DailySideAvailability availability);
}







