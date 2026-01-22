namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

/// <summary>
/// Request model for patching a location
/// </summary>
public record PatchLocationRequest
{
    public string? Name { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public string? AddressLine { get; init; }
    public LocationType? Type { get; init; }
    public bool? IsActive { get; init; }
    public string? ClientPublicId { get; init; }
    public string? ClientGroupPublicId { get; init; }
    public bool? UnassignClient { get; init; }
    public bool? UnassignClientGroup { get; init; }
}

/// <summary>
/// Query parameters for filtering locations
/// </summary>
public record LocationQueryParameters
{
    public bool? Active { get; init; }
    public bool? Company { get; init; }
    public bool? DeliveryDestinations { get; init; }
    public LocationType? Type { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public double? RadiusKm { get; init; }
}







