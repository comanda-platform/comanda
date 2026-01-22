namespace Comanda.Client.Kitchen.Infrastructure.ApiClients.ApiModels;

public record DailyMenuResponse(
    string PublicId,
    DateOnly Date,
    string? LocationPublicId,
    IEnumerable<DailyMenuItemResponse> Items,
    DateTime CreatedAt);

public record DailyMenuItemResponse(
    string PublicId,
    string ProductPublicId,
    string ProductName,
    int SequenceOrder,
    string? OverriddenName,
    decimal? OverriddenPrice,
    decimal CurrentPrice);







