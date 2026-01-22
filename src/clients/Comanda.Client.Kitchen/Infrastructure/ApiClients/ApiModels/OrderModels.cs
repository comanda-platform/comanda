namespace Comanda.Client.Kitchen.Infrastructure.ApiClients.ApiModels;

using Comanda.Shared.Enums;

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

// Container type and sides are now set at order line creation time
// Plating just marks the line as plated (no request body needed)







