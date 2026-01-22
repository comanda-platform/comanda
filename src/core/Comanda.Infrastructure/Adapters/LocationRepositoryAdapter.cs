namespace Comanda.Infrastructure.Adapters;

using Microsoft.EntityFrameworkCore;
using Comanda.Database;
using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Domain.StateMachines;
using Comanda.Infrastructure.Mappers;
using Comanda.Domain;

public class LocationRepositoryAdapter(
    Database.Repositories.ILocationRepository databaseRepository,
    Context context) : Domain.Repositories.ILocationRepository
{
    private readonly Database.Repositories.ILocationRepository _databaseRepository = databaseRepository;
    private readonly Context _context = context;

    public async Task<Location?> GetByIdAsync(int id)
    {
        var entity = await _databaseRepository.GetByIdAsync(id);

        return entity?.FromPersistence();
    }

    public async Task<Location?> GetByPublicIdAsync(string publicId)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(publicId);

        return entity?.FromPersistence();
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Location>> GetActiveLocationsAsync()
    {
        var entities = await _databaseRepository.GetActiveLocationsAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Location>> GetByClientIdAsync(int clientId)
    {
        var entities = await _databaseRepository.GetByClientIdAsync(clientId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Location>> GetByClientGroupIdAsync(int clientGroupId)
    {
        var entities = await _databaseRepository.GetByClientGroupIdAsync(clientGroupId);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Location>> GetByTypeAsync(LocationType type)
    {
        var entities = await _databaseRepository.GetByTypeAsync((int)type);

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Location>> GetCompanyLocationsAsync()
    {
        var entities = await _databaseRepository.GetCompanyLocationsAsync();

        return entities.Select(e => e.FromPersistence());
    }

    public async Task<IEnumerable<Location>> GetDeliveryDestinationsAsync()
    {
        var entities = await _databaseRepository.GetAllAsync();

        return entities
            .Select(e => e.FromPersistence())
            .Where(l => LocationStateMachine.CanBeDeliveryTarget(l.Type));
    }

    public async Task<IEnumerable<Location>> GetLocationsWithinRadiusAsync(
        double latitude,
        double longitude,
        double radiusKm)
    {
        var entities = await _databaseRepository.GetLocationsWithCoordinatesAsync();
        var locations = entities.Select(e => e.FromPersistence()).ToList();

        // Filter by distance
        return locations.Where(l =>
        {
            var distance = l.CalculateDistanceInKilometers(latitude, longitude);

            return distance.HasValue && distance.Value <= radiusKm;
        });
    }

    public async Task AddAsync(Location location)
    {
        var entity = location.ToPersistence();

        await _databaseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Location location)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(location.PublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Location, location.PublicId);

        location.UpdatePersistence(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Location location)
    {
        var entity = await _databaseRepository.GetByPublicIdAsync(location.PublicId)
            ?? throw new InvalidOperationException($"Location {location.PublicId} not found");

        await _databaseRepository.DeleteAsync(entity);
    }
}






