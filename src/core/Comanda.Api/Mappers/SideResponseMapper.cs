using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class SideResponseMapper
{
    public static SideResponse ToResponse(Domain.Entities.Side side) 
        => new(
            side.PublicId,
            side.Name,
            side.IsActive);
}






