namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

/// <summary>
/// Request model for patching an order (unified status updates and field changes)
/// </summary>
public record PatchOrderRequest
{
    public OrderStatus? Status { get; init; }
    public string? DeliveryLocationPublicId { get; init; }
    public string? ChangedByPublicId { get; init; }
}

/// <summary>
/// Request model for patching an order line status
/// </summary>
public record PatchOrderLineRequest
{
    public OrderLineStatus? Status { get; init; }
}

/// <summary>
/// Enum for order line status
/// </summary>
public enum OrderLineStatus
{
    Pending,
    Preparing,
    Plated
}

/// <summary>
/// Query parameters for filtering orders
/// </summary>
public record OrderQueryParameters
{
    public bool? Active { get; init; }
    public string? ClientId { get; init; }
    public string? ClientGroupId { get; init; }
    public OrderStatus? Status { get; init; }
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }
}







