namespace Comanda.Infrastructure.Database.Repositories;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Database.Entities;

public class LocationRepository(Context context)
    : GenericDatabaseRepository<LocationDatabaseEntity>(context), ILocationRepository
{
    public override async Task<LocationDatabaseEntity?> GetByPublicIdAsync(string publicId) =>
        await Query().FirstOrDefaultAsync(l => l.PublicId == publicId);

    public async Task<IEnumerable<LocationDatabaseEntity>> GetActiveLocationsAsync() =>
        await Query()
            .Where(l => l.IsActive)
            .ToListAsync();

    public async Task<IEnumerable<LocationDatabaseEntity>> GetByClientIdAsync(int clientId) =>
        await Query()
            .Where(l => l.ClientId == clientId)
            .ToListAsync();

    public async Task<IEnumerable<LocationDatabaseEntity>> GetByClientGroupIdAsync(int clientGroupId) =>
        await Query()
            .Where(l => l.ClientGroupId == clientGroupId)
            .ToListAsync();

    public async Task<IEnumerable<LocationDatabaseEntity>> GetByTypeAsync(int locationTypeId) =>
        await Query()
            .Where(l => l.LocationTypeId == locationTypeId)
            .ToListAsync();

    public async Task<IEnumerable<LocationDatabaseEntity>> GetCompanyLocationsAsync() =>
        await Query()
            .Where(l => l.LocationTypeId == 3 || l.LocationTypeId == 4 || l.LocationTypeId == 5) // OurRestaurant, OurKitchen, OurWarehouse
            .ToListAsync();

    public async Task<IEnumerable<LocationDatabaseEntity>> GetLocationsWithCoordinatesAsync() =>
        await Query()
            .Where(l => l.Latitude.HasValue && l.Longitude.HasValue)
            .ToListAsync();
}






