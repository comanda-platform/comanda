using Comanda.Shared.Enums;

namespace Comanda.Client.Admin.Models;

public record AuthorizationResponse(
    string PublicId,
    string PersonPublicId,
    string PersonName,
    string AccountPublicId,
    string AccountName,
    AuthorizationRole Role,
    bool IsActive,
    DateTime CreatedAt);

public record CreateAuthorizationRequest(
    string PersonPublicId,
    string AccountPublicId,
    AuthorizationRole Role = AuthorizationRole.Orderer);

public record PatchAuthorizationRequest
{
    public AuthorizationRole? Role { get; init; }
    public bool? IsActive { get; init; }
}
