namespace Comanda.Api.Models;

public record CreateClientGroupRequest(
    string Name,
    bool HasCreditLine = false);

public record UpdateClientGroupRequest(string Name);

public record ClientGroupResponse(
    string PublicId,
    string Name,
    bool HasCreditLine,
    int MemberCount);







