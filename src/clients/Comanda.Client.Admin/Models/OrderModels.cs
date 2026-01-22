using Comanda.Shared.Enums;

namespace Comanda.Client.Admin.Models;

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
    IEnumerable<string> SelectedSides);

public record CreateOrderRequest(
    OrderFulfillmentType FulfillmentType,
    OrderSource Source,
    string? ClientPublicId,
    string? ClientGroupPublicId,
    string? LocationPublicId);

public record AddOrderLineRequest(
    string ProductPublicId,
    int Quantity,
    string? ClientPublicId,
    IEnumerable<string>? SelectedSides);

public record PatchOrderRequest(
    OrderStatus? Status,
    string? DeliveryLocationPublicId,
    string? ChangedByPublicId);

public record PatchOrderLineRequest(
    OrderLineStatus? Status);

public enum OrderLineStatus
{
    Pending,
    Preparing,
    Plated
}










