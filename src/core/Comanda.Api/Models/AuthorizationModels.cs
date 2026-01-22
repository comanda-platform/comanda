namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

public record CreateAuthorizationRequest(
    string PersonPublicId,
    string AccountPublicId,
    AuthorizationRole Role = AuthorizationRole.Orderer);

public record UpdateAuthorizationRequest(AuthorizationRole Role);

public record UpdateAuthorizationStatusRequest(bool IsActive);

public record AuthorizationQueryParameters
{
    public string? PersonPublicId { get; init; }
    public string? AccountPublicId { get; init; }
    public bool? ActiveOnly { get; init; }
}

public record PatchAuthorizationRequest
{
    public AuthorizationRole? Role { get; init; }
    public bool? IsActive { get; init; }
}

public record AuthorizationResponse(
    string PublicId,
    string PersonPublicId,
    string PersonName,
    string AccountPublicId,
    string AccountName,
    AuthorizationRole Role,
    bool IsActive,
    DateTime CreatedAt);
