namespace Comanda.Client.Delivery.Infrastructure.ApiClients.ApiModels;

using Comanda.Shared.Enums;
using System.Text.Json.Serialization;

public record OrderResponse(
    string PublicId,
    string? ClientPublicId,
    string? ClientGroupPublicId,
    string? LocationPublicId,
    OrderSource Source,
    OrderFulfillmentType FulfillmentType,
    OrderStatus Status,
    decimal TotalAmount,
    int TotalItems,
    IEnumerable<OrderLineResponse> Lines,
    DateTime CreatedAt);

public record OrderLineResponse(
    string PublicId,
    string ProductPublicId,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal,
    OrderLinePrepStatus PrepStatus,
    DateTime? PrepStartedAt,
    DateTime? PrepCompletedAt,
    string? ContainerType,
    IReadOnlyList<string>? SelectedSides);

public record LocationResponse(
    [property: JsonPropertyName("publicId")]
    string PublicId,

    [property: JsonPropertyName("name")]
    string? Name,

    [property: JsonPropertyName("addressLine")]
    string? AddressLine,

    [property: JsonPropertyName("latitude")]
    double? Latitude,

    [property: JsonPropertyName("longitude")]
    double? Longitude,

    // Renamed + correct type
    [property: JsonPropertyName("type")]
    int LocationType);








