using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class AuthorizationResponseMapper
{
    public static AuthorizationResponse ToResponse(
        Domain.Entities.Authorization authorization,
        string personName,
        string accountName)
        => new(
            authorization.PublicId,
            authorization.PersonPublicId,
            personName,
            authorization.AccountPublicId,
            accountName,
            authorization.Role,
            authorization.IsActive,
            authorization.CreatedAt);
}
