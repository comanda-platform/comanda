namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class DailyMenuRepository(Context context) : GenericDatabaseRepository<DailyMenuDatabaseEntity>(context), IDailyMenuRepository
{
    public override async Task<DailyMenuDatabaseEntity?> GetByPublicIdAsync(string publicId) => 
        await Query().FirstOrDefaultAsync(m => m.PublicId == publicId);

    public async Task<DailyMenuDatabaseEntity?> GetByDateAsync(
        DateOnly date, 
        int? locationId = null,
        string? locationPublicId = null)
    {
        var query = Query().Where(m => m.Date == date);

        if (locationId.HasValue)
            query = query.Where(m => m.LocationId == locationId);

        else if (!string.IsNullOrEmpty(locationPublicId))
            query = query.Where(m => m.Location != null && m.Location.PublicId == locationPublicId);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<DailyMenuDatabaseEntity>> GetByDateRangeAsync(
        DateOnly from,
        DateOnly to,
        int? locationId = null,
        string? locationPublicId = null)
    {
        var query = Query().Where(m => m.Date >= from && m.Date <= to);

        if (locationId.HasValue)
            query = query.Where(m => m.LocationId == locationId);

        else if (!string.IsNullOrEmpty(locationPublicId))
            query = query.Where(m => m.Location.PublicId == locationPublicId);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<DailyMenuDatabaseEntity>> GetByLocationIdAsync(int locationId) => 
        await Query()
            .Where(m => m.LocationId == locationId)
            .ToListAsync();
}







