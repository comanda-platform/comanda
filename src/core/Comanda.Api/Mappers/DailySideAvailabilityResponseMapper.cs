using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class DailySideAvailabilityResponseMapper
{
    public static DailySideAvailabilityResponse ToResponse(Domain.Entities.DailySideAvailability availability) 
        => new(
            availability.Side.PublicId,
            availability.Side.Name,
            availability.Date,
            availability.IsAvailable);
}






