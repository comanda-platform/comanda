using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public class LocationResponseMapper
{
    public static LocationResponse ToResponse(Domain.Entities.Location location) 
        => new(
            location.PublicId,
            location.Name,
            location.Type,
            location.Latitude,
            location.Longitude,
            location.AddressLine,
            location.IsActive,
            location.ClientPublicId ?? string.Empty,
            location.ClientGroupPublicId ?? string.Empty,
            location.CreatedAt);
}







