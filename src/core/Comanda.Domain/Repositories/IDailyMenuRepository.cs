namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;

public interface IDailyMenuRepository
{
    Task<DailyMenu?> GetByIdAsync(int id);
    Task<DailyMenu?> GetByPublicIdAsync(string publicId);
    Task<DailyMenu?> GetByDateAsync(DateOnly date, int? locationId = null, string? locationPublicId = null);
    Task<IEnumerable<DailyMenu>> GetAllAsync();
    Task<IEnumerable<DailyMenu>> GetByDateRangeAsync(DateOnly from, DateOnly to, int? locationId = null, string? locationPublicId = null);
    Task<IEnumerable<DailyMenu>> GetByLocationIdAsync(int locationId);
    Task<IEnumerable<DailyMenu>> GetByLocationPublicIdAsync(string locationPublicId);
    Task AddAsync(DailyMenu menu);
    Task UpdateAsync(DailyMenu menu);
    Task DeleteAsync(DailyMenu menu);
}







