namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

public record CreateUnitRequest(
    string Code,
    string Name,
    UnitCategory Category,
    decimal ToBaseMultiplier);

public record UpdateUnitRequest(
    string Code,
    string Name,
    UnitCategory Category,
    decimal ToBaseMultiplier);

public record UnitResponse(
    string PublicId,
    string Code,
    string Name,
    UnitCategory Category,
    decimal? ToBaseMultiplier);







