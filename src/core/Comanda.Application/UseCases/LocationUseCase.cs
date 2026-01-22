namespace Comanda.Application.UseCases;

using Comanda.Domain.Entities;
using Comanda.Shared.Enums;
using Comanda.Domain.Repositories;
using Comanda.Domain.StateMachines;
using Comanda.Domain;

public class LocationUseCase(
    ILocationRepository locationRepository,
    IClientRepository clientRepository,
    IClientGroupRepository clientGroupRepository) : UseCaseBase(EntityTypePrintNames.Location)
{
    private readonly ILocationRepository _locationRepository = locationRepository;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IClientGroupRepository _clientGroupRepository = clientGroupRepository;

    public async Task<Location> CreateLocationAsync(
        LocationType type,
        string? name = null,
        double? latitude = null,
        double? longitude = null,
        string? addressLine = null,
        string? clientPublicId = null,
        string? clientGroupPublicId = null)
    {
        Client? client = null;
        ClientGroup? clientGroup = null;

        if (!string.IsNullOrEmpty(clientPublicId))
        {
            client = await _clientRepository.GetByPublicIdAsync(clientPublicId)
                ?? throw new NotFoundException(EntityTypePrintNames.Client, clientPublicId);
        }

        if (!string.IsNullOrEmpty(clientGroupPublicId))
        {
            clientGroup = await _clientGroupRepository.GetByPublicIdAsync(clientGroupPublicId)
                ?? throw new NotFoundException(EntityTypePrintNames.ClientGroup, clientGroupPublicId);
        }

        var location = new Location(
            type,
            name,
            latitude,
            longitude,
            addressLine,
            client,
            clientGroup);

        await _locationRepository.AddAsync(location);
        return location;
    }

    public async Task<Location> GetLocationByPublicIdAsync(string publicId)
        => await _locationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        => await _locationRepository.GetAllAsync();

    public async Task<IEnumerable<Location>> GetActiveLocationsAsync()
        => await _locationRepository.GetActiveLocationsAsync();

    public async Task<IEnumerable<Location>> GetLocationsByClientIdAsync(int clientId)
        => await _locationRepository.GetByClientIdAsync(clientId);

    public async Task<IEnumerable<Location>> GetLocationsByClientGroupIdAsync(int clientGroupId)
        => await _locationRepository.GetByClientGroupIdAsync(clientGroupId);

    public async Task<IEnumerable<Location>> GetLocationsByTypeAsync(LocationType type)
        => await _locationRepository.GetByTypeAsync(type);

    public async Task<IEnumerable<Location>> GetCompanyLocationsAsync()
        => await _locationRepository.GetCompanyLocationsAsync();

    public async Task<IEnumerable<Location>> GetDeliveryDestinationsAsync()
        => await _locationRepository.GetDeliveryDestinationsAsync();

    public async Task<IEnumerable<Location>> GetNearbyLocationsAsync(
        double latitude,
        double longitude,
        double radiusKm)
    {
        return await _locationRepository.GetLocationsWithinRadiusAsync(
            latitude,
            longitude,
            radiusKm);
    }

    public async Task UpdateLocationDetailsAsync(
        string publicId,
        string? name,
        double? latitude,
        double? longitude,
        string? addressLine)
    {
        var location = await _locationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        location.UpdateDetails(name, latitude, longitude, addressLine);
        await _locationRepository.UpdateAsync(location);
    }

    public async Task UpdateLocationTypeAsync(string publicId, LocationType newType)
    {
        var location = await _locationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        if (!LocationStateMachine.CanTransitionTo(location.Type, newType))
            throw new InvalidOperationException(
                $"Cannot change location type from {location.Type} to {newType}");

        location.UpdateType(newType);
        await _locationRepository.UpdateAsync(location);
    }

    public async Task AssignLocationToClientAsync(string publicId, string clientPublicId)
    {
        var location = await _locationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        var client = await _clientRepository.GetByPublicIdAsync(clientPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.Client, clientPublicId);

        location.AssignToClient(client);
        await _locationRepository.UpdateAsync(location);
    }

    public async Task AssignLocationToClientGroupAsync(string publicId, string clientGroupPublicId)
    {
        var location = await _locationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        var group = await _clientGroupRepository.GetByPublicIdAsync(clientGroupPublicId)
            ?? throw new NotFoundException(EntityTypePrintNames.ClientGroup, clientGroupPublicId);

        location.AssignToClientGroup(group);
        await _locationRepository.UpdateAsync(location);
    }

    public async Task UnassignLocationFromClientAsync(string publicId)
    {
        var location = await _locationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        location.UnassignFromClient();
        await _locationRepository.UpdateAsync(location);
    }

    public async Task UnassignLocationFromClientGroupAsync(string publicId)
    {
        var location = await _locationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        location.UnassignFromClientGroup();
        await _locationRepository.UpdateAsync(location);
    }

    public async Task ActivateLocationAsync(string publicId)
    {
        var location = await _locationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        location.Activate();
        await _locationRepository.UpdateAsync(location);
    }

    public async Task DeactivateLocationAsync(string publicId)
    {
        var location = await _locationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        location.Deactivate();
        await _locationRepository.UpdateAsync(location);
    }

    public async Task<double?> CalculateDistanceAsync(
        string publicId,
        double targetLatitude,
        double targetLongitude)
    {
        var location = await _locationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        return location.CalculateDistanceInKilometers(targetLatitude, targetLongitude);
    }

    public async Task DeleteLocationAsync(string publicId)
    {
        var location = await _locationRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _locationRepository.DeleteAsync(location);
    }
}







