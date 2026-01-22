namespace Comanda.Api.Models;

public record CreateSideRequest(string Name);

public record UpdateSideRequest(string Name);

public record SideResponse(
    string PublicId,
    string Name,
    bool IsActive);







