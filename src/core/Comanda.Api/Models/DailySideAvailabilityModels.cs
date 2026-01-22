namespace Comanda.Api.Models;

public record SetSideAvailabilityRequest(
    string SidePublicId,
    DateOnly Date,
    bool IsAvailable);

public record DailySideAvailabilityResponse(
    string SidePublicId,
    string SideName,
    DateOnly Date,
    bool IsAvailable);







