namespace Comanda.Api.Models;

public record CreateDailyMenuRequest(
    DateOnly Date,
    string LocationPublicId);

public record AddDailyMenuItemRequest(
    string ProductPublicId,
    int SequenceOrder,
    string? OverriddenName = null,
    decimal? OverriddenPrice = null);

public record UpdateDailyMenuItemRequest(
    int? SequenceOrder = null,
    string? OverriddenName = null,
    decimal? OverriddenPrice = null);

public record DailyMenuResponse(
    string PublicId,
    DateOnly Date,
    string LocationPublicId,
    IEnumerable<DailyMenuItemResponse> Items,
    DateTime CreatedAt);

public record DailyMenuItemResponse(
    string PublicId,
    string ProductPublicId,
    string ProductName,
    int SequenceOrder,
    string? OverriddenName,
    decimal? OverriddenPrice,
    decimal EffectivePrice);







