using Comanda.Shared.Enums;

namespace Comanda.Client.Admin.Models;

public record ClientResponse(
    string PublicId,
    string Name,
    string? ClientGroupPublicId,
    IEnumerable<ClientContactResponse> Contacts,
    IEnumerable<LocationResponse> Locations);

public record ClientContactResponse(
    string PublicId,
    ClientContactType Type,
    string Value);

public record LocationResponse(
    string PublicId,
    string? Name,
    double? Latitude,
    double? Longitude,
    string? AddressLine,
    bool IsActive,
    LocationType Type,
    string? ClientPublicId,
    string? ClientGroupPublicId,
    DateTime CreatedAt);

public record CreateClientRequest(
    string Name,
    string? ClientGroupPublicId);

public record UpdateClientRequest(
    string Name,
    string? ClientGroupPublicId);

public record AddClientContactRequest(
    ClientContactType Type,
    string Value);

public record CreateLocationRequest(
    LocationType Type,
    string? Name,
    double? Latitude,
    double? Longitude,
    string? AddressLine,
    string? ClientPublicId,
    string? ClientGroupPublicId);

public record UpdateLocationRequest(
    string? Name,
    double? Latitude,
    double? Longitude,
    string? AddressLine);

public static class LocationExtensions
{
    public static bool HasCoordinates(this LocationResponse location)
    {
        return location.Latitude.HasValue && location.Longitude.HasValue;
    }
}










