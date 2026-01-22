namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

public record CreateLocationRequest(
    LocationType Type,
    string Name,
    double Latitude,
    double Longitude,
    string ClientPublicId,
    string ClientGroupPublicId,
    string? AddressLine = null);

public record UpdateLocationRequest(
    string Name,
    double Latitude,
    double Longitude,
    string? AddressLine = null);

public record UpdateLocationTypeRequest(
    LocationType Type);

public record AssignLocationToClientRequest(
    string ClientPublicId);

public record AssignLocationToClientGroupRequest(
    string ClientGroupPublicId);

public record CalculateDistanceRequest(
    double TargetLatitude,
    double TargetLongitude);

public record GetNearbyLocationsRequest(
    double Latitude,
    double Longitude,
    double RadiusKm);

public record DistanceResponse(
    double? DistanceKm);

public record LocationResponse(
    string PublicId,
    string? Name,
    LocationType Type,
    double? Latitude,
    double? Longitude,
    string? AddressLine,
    bool IsActive,
    string ClientPublicId,
    string ClientGroupPublicId,
    DateTime CreatedAt);







