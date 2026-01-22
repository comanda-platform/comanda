namespace Comanda.Domain.Repositories;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;

public interface ILocationRepository
{
    Task<Location?> GetByIdAsync(int id);
    Task<Location?> GetByPublicIdAsync(string publicId);
    Task<IEnumerable<Location>> GetAllAsync();
    Task<IEnumerable<Location>> GetActiveLocationsAsync();
    Task<IEnumerable<Location>> GetByClientIdAsync(int clientId);
    Task<IEnumerable<Location>> GetByClientGroupIdAsync(int clientGroupId);
    Task<IEnumerable<Location>> GetByTypeAsync(LocationType type);
    Task<IEnumerable<Location>> GetCompanyLocationsAsync();
    Task<IEnumerable<Location>> GetDeliveryDestinationsAsync();
    Task<IEnumerable<Location>> GetLocationsWithinRadiusAsync(double latitude, double longitude, double radiusKm);
    Task AddAsync(Location location);
    Task UpdateAsync(Location location);
    Task DeleteAsync(Location location);
}






