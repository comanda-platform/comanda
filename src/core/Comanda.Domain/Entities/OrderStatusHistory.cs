namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;
using Comanda.Shared.Enums;

public class OrderStatusHistory
{
    public string PublicId { get; private set; }
    public string OrderPublicId { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime ChangedAt { get; private set; }
    public string? ChangedByPublicId { get; private set; }

    private OrderStatusHistory() { } // For reflection / serializers

    private OrderStatusHistory(
        string publicId,
        string orderPublicId,
        OrderStatus status,
        DateTime changedAt,
        string? changedByPublicId)
    {
        PublicId = publicId;
        OrderPublicId = orderPublicId;
        Status = status;
        ChangedAt = changedAt;
        ChangedByPublicId = changedByPublicId;
    }

    // Domain-level constructor using public ids
    public OrderStatusHistory(
        OrderStatus status,
        DateTime changedAt,
        string orderPublicId,
        string? changedByPublicId = null)
    {
        PublicId = PublicIdHelper.Generate();
        Status = status;
        ChangedAt = changedAt;
        OrderPublicId = orderPublicId;
        ChangedByPublicId = changedByPublicId;
    }

    public OrderStatusHistory(
        OrderStatus status,
        DateTime changedAt,
        string? changedByPublicId = null)
    {
        PublicId = PublicIdHelper.Generate();
        Status = status;
        ChangedAt = changedAt;
        ChangedByPublicId = changedByPublicId;
    }

    public static OrderStatusHistory Rehydrate(
        string publicId,
        string orderPublicId,
        OrderStatus status,
        DateTime changedAt,
        string? changedByPublicId)
        => new(
            publicId,
            orderPublicId,
            status,
            changedAt,
            changedByPublicId);
}







