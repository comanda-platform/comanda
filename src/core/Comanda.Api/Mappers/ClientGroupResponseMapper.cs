using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class ClientGroupResponseMapper
{
    public static ClientGroupResponse ToResponse(Domain.Entities.ClientGroup group) 
        => new(
            group.PublicId,
            group.Name,
            group.HasCreditLine,
            group.Members.Count);
}






