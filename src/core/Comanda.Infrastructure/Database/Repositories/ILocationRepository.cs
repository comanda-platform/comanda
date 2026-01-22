namespace Comanda.Infrastructure.Database.Repositories;

using Comanda.Database.Entities;

public interface ILocationRepository : IGenericDatabaseRepository<LocationDatabaseEntity>
{
    Task<LocationDatabaseEntity?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<LocationDatabaseEntity>> GetActiveLocationsAsync();
    Task<IEnumerable<LocationDatabaseEntity>> GetByClientIdAsync(int clientId);
    Task<IEnumerable<LocationDatabaseEntity>> GetByClientGroupIdAsync(int clientGroupId);
    Task<IEnumerable<LocationDatabaseEntity>> GetByTypeAsync(int locationTypeId);
    Task<IEnumerable<LocationDatabaseEntity>> GetCompanyLocationsAsync();
    Task<IEnumerable<LocationDatabaseEntity>> GetLocationsWithCoordinatesAsync();
}






