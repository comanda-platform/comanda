namespace Comanda.Client.Kitchen.Infrastructure.ApiClients.ApiModels;

public record DailySideAvailabilityResponse(
    string SidePublicId,
    string SideName,
    DateOnly Date,
    bool IsAvailable);

public record SideResponse(
    string PublicId,
    string Name,
    bool IsActive);







