namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class DailySideAvailabilityRepository(Context context) : GenericDatabaseRepository<DailySideAvailabilityDatabaseEntity>(context), IDailySideAvailabilityRepository
{
    public override async Task<DailySideAvailabilityDatabaseEntity?> GetByPublicIdAsync(string publicId) => 
        await Query().FirstOrDefaultAsync(a => a.PublicId == publicId);

    //public async Task<DailySideAvailabilityDatabaseEntity?> GetBySideIdAndDateAsync(int sideId, DateOnly date) =>
    //    await Query().FirstOrDefaultAsync(a => 
    //        a.SideId == sideId 
    //        && a.Date == date);

    //public async Task<DailySideAvailabilityDatabaseEntity?> GetBySidePublicIdAndDateAsync(string sidePublicId, DateOnly date) => 
    //    await Query().FirstOrDefaultAsync(a => 
    //        a.Side.PublicId == sidePublicId
    //        && a.Date == date);
        
    public async Task<IEnumerable<DailySideAvailabilityDatabaseEntity>> GetByDateAsync(DateOnly date) => 
        await Query()
            .Where(a => a.Date == date)
            .ToListAsync();

    //public async Task<IEnumerable<DailySideAvailabilityDatabaseEntity>> GetBySideIdAsync(int sideId) => 
    //    await Query()
    //        .Where(a => a.SideId == sideId)
    //        .ToListAsync();

    public async Task<IEnumerable<DailySideAvailabilityDatabaseEntity>> GetAvailableByDateAsync(DateOnly date) => 
        await Query()
            .Where(a => 
                a.Date == date 
                && a.IsAvailable)
            .ToListAsync();
}







