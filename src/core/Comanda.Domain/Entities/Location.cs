namespace Comanda.Domain.Entities;

using Comanda.Shared.Enums;
using Comanda.Domain.Helpers;

public class Location
{
    public string PublicId { get; private set; }
    public string? Name { get; private set; }
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }
    public string? AddressLine { get; private set; }
    public bool IsActive { get; private set; }
    public LocationType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Relationships
    public string? ClientPublicId { get; private set; }
    public string? ClientGroupPublicId { get; private set; }

    private Location() { } // For reflection / serializers

    private Location(
        string publicId,
        string? name,
        double? latitude,
        double? longitude,
        string? addressLine,
        bool isActive,
        LocationType type,
        string? clientPublicId,
        string? clientGroupPublicId,
        DateTime createdAt)
    {
        PublicId = publicId;
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        AddressLine = addressLine;
        IsActive = isActive;
        Type = type;
        ClientPublicId = clientPublicId;
        ClientGroupPublicId = clientGroupPublicId;
        CreatedAt = createdAt;
    }

    public Location(
        LocationType type,
        string? name = null,
        double? latitude = null,
        double? longitude = null,
        string? addressLine = null,
        Client? client = null,
        ClientGroup? clientGroup = null)
    {
        // Validate coordinates if provided
        if (latitude.HasValue && (latitude.Value < -90 || latitude.Value > 90))
            throw new ArgumentException("Latitude must be between -90 and 90", nameof(latitude));

        if (longitude.HasValue && (longitude.Value < -180 || longitude.Value > 180))
            throw new ArgumentException("Longitude must be between -180 and 180", nameof(longitude));

        // Validate that at least one location identifier is provided
        if (string.IsNullOrWhiteSpace(name) &&
            string.IsNullOrWhiteSpace(addressLine) &&
            !latitude.HasValue &&
            !longitude.HasValue)
            throw new ArgumentException("Location must have at least a name, address, or coordinates");

        // Business rules for ownership - use public ids when available
        ValidateOwnership(type, client?.PublicId, clientGroup?.PublicId);

        PublicId = PublicIdHelper.Generate();
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        AddressLine = addressLine;
        IsActive = true;
        Type = type;
        CreatedAt = DateTime.UtcNow;

        if (client != null)
        {
            ClientPublicId = client.PublicId;
        }

        if (clientGroup != null)
        {
            ClientGroupPublicId = clientGroup.PublicId;
        }
    }

    public void UpdateDetails(
        string? name,
        double? latitude,
        double? longitude,
        string? addressLine)
    {
        // Validate coordinates if provided
        if (latitude.HasValue && (latitude.Value < -90 || latitude.Value > 90))
            throw new ArgumentException("Latitude must be between -90 and 90", nameof(latitude));

        if (longitude.HasValue && (longitude.Value < -180 || longitude.Value > 180))
            throw new ArgumentException("Longitude must be between -180 and 180", nameof(longitude));

        // Validate that at least one location identifier is provided
        if (string.IsNullOrWhiteSpace(name) &&
            string.IsNullOrWhiteSpace(addressLine) &&
            !latitude.HasValue &&
            !longitude.HasValue)
            throw new ArgumentException("Location must have at least a name, address, or coordinates");

        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        AddressLine = addressLine;
    }

    public void UpdateType(LocationType newType)
    {
        ValidateOwnership(newType, ClientPublicId, ClientGroupPublicId);

        Type = newType;
    }

    public void AssignToClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);

        if (Type == LocationType.OurRestaurant ||
            Type == LocationType.OurKitchen ||
            Type == LocationType.OurWarehouse)
            throw new InvalidOperationException("Company locations cannot be assigned to clients");

        if (ClientGroupPublicId != null)
            throw new InvalidOperationException("Location is already assigned to a client group. Unassign from group first.");

        ClientPublicId = client.PublicId;
    }

    public void AssignToClientGroup(ClientGroup clientGroup)
    {
        ArgumentNullException.ThrowIfNull(clientGroup);

        if (Type == LocationType.OurRestaurant ||
            Type == LocationType.OurKitchen ||
            Type == LocationType.OurWarehouse)
            throw new InvalidOperationException("Company locations cannot be assigned to client groups");

        if (ClientPublicId != null)
            throw new InvalidOperationException("Location is already assigned to a client. Unassign from client first.");

        ClientGroupPublicId = clientGroup.PublicId;
    }

    public void UnassignFromClient()
    {
        if (string.IsNullOrEmpty(ClientPublicId))
            throw new InvalidOperationException("Location is not assigned to any client");

        ClientPublicId = null;
    }

    public void UnassignFromClientGroup()
    {
        if (string.IsNullOrEmpty(ClientGroupPublicId))
            throw new InvalidOperationException("Location is not assigned to any client group");

        ClientGroupPublicId = null;
    }

    public void Activate()
    {
        if (IsActive)
            throw new InvalidOperationException("Location is already active");

        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("Location is already inactive");

        IsActive = false;
    }

    public double? CalculateDistanceInKilometers(
        double targetLatitude,
        double targetLongitude)
    {
        if (!Latitude.HasValue || !Longitude.HasValue)
            return null;

        // Haversine formula
        var earthRadiusKm = 6371.0;

        var lat1Rad = (double)Latitude.Value * Math.PI / 180;
        var lat2Rad = (double)targetLatitude * Math.PI / 180;
        var deltaLat = ((double)targetLatitude - (double)Latitude.Value) * Math.PI / 180;
        var deltaLon = ((double)targetLongitude - (double)Longitude.Value) * Math.PI / 180;

        var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return earthRadiusKm * c;
    }

    public bool HasCoordinates() => Latitude.HasValue && Longitude.HasValue;

    public bool IsCompanyLocation() =>
        Type == LocationType.OurRestaurant ||
        Type == LocationType.OurKitchen ||
        Type == LocationType.OurWarehouse;

    private static void ValidateOwnership(
        LocationType type,
        string? clientPublicId,
        string? clientGroupPublicId)
    {
        // Company locations cannot be assigned to clients/groups
        if (type == LocationType.OurRestaurant ||
            type == LocationType.OurKitchen ||
            type == LocationType.OurWarehouse)
        {
            if (!string.IsNullOrEmpty(clientPublicId) || !string.IsNullOrEmpty(clientGroupPublicId))
                throw new ArgumentException("Company locations cannot be assigned to clients or client groups");
        }

        // Client/Business locations should be assigned to either client or group
        if (type == LocationType.ClientHome || type == LocationType.Business)
        {
            if (string.IsNullOrEmpty(clientPublicId) && string.IsNullOrEmpty(clientGroupPublicId))
                throw new ArgumentException("Client/Business locations must be assigned to either a client or client group");

            if (!string.IsNullOrEmpty(clientPublicId) && !string.IsNullOrEmpty(clientGroupPublicId))
                throw new ArgumentException("Location cannot be assigned to both client and client group");
        }
    }

    public static Location Rehydrate(
        string publicId,
        string? name,
        double? latitude,
        double? longitude,
        string? addressLine,
        bool isActive,
        LocationType type,
        string? clientPublicId,
        string? clientGroupPublicId,
        DateTime createdAt)
        => new(
            publicId,
            name,
            latitude,
            longitude,
            addressLine,
            isActive,
            type,
            clientPublicId,
            clientGroupPublicId,
            createdAt);
}






