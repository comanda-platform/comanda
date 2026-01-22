namespace Comanda.Domain.Entities;

using Comanda.Shared.Enums;
using Comanda.Domain.Helpers;
using Comanda.Domain.StateMachines;

public class Order
{
    public string PublicId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus Status { get; private set; }
    public OrderFulfillmentType FulfillmentType { get; private set; }
    public OrderSource Source { get; private set; }

    // Domain-facing public ids
    public string? ClientPublicId { get; private set; }
    public string? ClientGroupPublicId { get; private set; }
    public string? LocationPublicId { get; private set; }

    private readonly List<OrderLine> _lines = new();
    public IReadOnlyCollection<OrderLine> Lines => _lines.AsReadOnly();

    private readonly List<OrderStatusHistory> _statusHistory = [];
    public IReadOnlyCollection<OrderStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    private Order()
    {
        // Ensure collections are initialized for Entity Framework
        _lines = new List<OrderLine>();
        _statusHistory = new List<OrderStatusHistory>();
    }

    // DB-compatible constructor
    private Order(
        string publicId,
        DateTime createdAt,
        OrderStatus status,
        OrderFulfillmentType fulfillmentType,
        OrderSource source,
        string? clientPublicId,
        string? clientGroupPublicId,
        string? locationPublicId)
    {
        PublicId = publicId;
        CreatedAt = createdAt;
        Status = status;
        FulfillmentType = fulfillmentType;
        Source = source;
        ClientPublicId = clientPublicId;
        ClientGroupPublicId = clientGroupPublicId;
        LocationPublicId = locationPublicId;
    }

    // Domain constructor using PublicIds
    public Order(
        OrderFulfillmentType fulfillmentType,
        OrderSource source,
        string? clientPublicId = null,
        string? clientGroupPublicId = null,
        string? locationPublicId = null)
    {
        // Validate delivery orders have a location
        if (fulfillmentType == OrderFulfillmentType.Delivery && string.IsNullOrWhiteSpace(locationPublicId))
            throw new ArgumentException("Delivery orders must have a delivery location", nameof(locationPublicId));

        // Validate that client or client group is provided for non-dine-in orders
        if (fulfillmentType != OrderFulfillmentType.DineIn && string.IsNullOrWhiteSpace(clientPublicId) && string.IsNullOrWhiteSpace(clientGroupPublicId))
            throw new ArgumentException("Take-away and delivery orders must have a client or client group");

        PublicId = PublicIdHelper.Generate();
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Created;
        FulfillmentType = fulfillmentType;
        Source = source;

        ClientPublicId = clientPublicId;
        ClientGroupPublicId = clientGroupPublicId;
        LocationPublicId = locationPublicId;

        _statusHistory.Add(new OrderStatusHistory(
            OrderStatus.Created,
            DateTime.UtcNow,
            PublicId, 
            null));
    }

    public void UpdateStatus(
        OrderStatus newStatus,
        string? changedByPublicId = null)
    {
        if (!OrderStateMachine.CanTransitionTo(Status, newStatus))
            throw new InvalidOperationException($"Cannot transition from {Status} to {newStatus}");

        // Validate delivery-specific transitions
        if (newStatus == OrderStatus.InTransit && FulfillmentType != OrderFulfillmentType.Delivery)
            throw new InvalidOperationException("Only delivery orders can be marked as in transit");

        Status = newStatus;

        _statusHistory.Add(new OrderStatusHistory(
            newStatus,
            DateTime.UtcNow,
            PublicId, 
            changedByPublicId));
    }

    public void Accept(string? changedByPublicId = null)
        => UpdateStatus(
            OrderStatus.Accepted,
            changedByPublicId);

    public void StartPreparing(string? changedByPublicId = null)
        => UpdateStatus(
            OrderStatus.Preparing,
            changedByPublicId);

    public void MarkReady(string? changedByPublicId = null)
        => UpdateStatus(
            OrderStatus.Ready,
            changedByPublicId);

    public void StartDelivery(string? changedByPublicId = null)
        => UpdateStatus(
            OrderStatus.InTransit,
            changedByPublicId);

    public void MarkDelivered(string? changedByPublicId = null)
        => UpdateStatus(
            OrderStatus.Delivered,
            changedByPublicId);

    public void Complete(string? changedByPublicId = null)
        => UpdateStatus(
            OrderStatus.Completed,
            changedByPublicId);

    public void Cancel(string? changedByPublicId = null)
    {
        if (!OrderStateMachine.CanBeCancelled(Status))
            throw new InvalidOperationException($"Order in status {Status} cannot be cancelled");

        UpdateStatus(OrderStatus.Cancelled, changedByPublicId);
    }

    public void AddLine(OrderLine line)
    {
        ArgumentNullException.ThrowIfNull(line);

        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Can only add lines to orders in Created status");

        _lines.Add(line);
    }

    public void RemoveLine(OrderLine line)
    {
        ArgumentNullException.ThrowIfNull(line);

        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Can only remove lines from orders in Created status");

        if (!_lines.Remove(line))
            throw new InvalidOperationException("Line not found in this order");
    }

    // Domain-level assign location by PublicId
    public void AssignLocation(string locationPublicId)
    {
        if (FulfillmentType != OrderFulfillmentType.Delivery)
            throw new InvalidOperationException("Only delivery orders can have a location assigned");

        LocationPublicId = locationPublicId;
    }

    public decimal TotalAmount => _lines.Sum(l => l.LineTotal);
    public int TotalItems => _lines.Sum(l => l.Quantity);
    public bool IsActive => OrderStateMachine.IsActiveStatus(Status);
    public bool CanBeModified => Status == OrderStatus.Created;

    // DB-compatible factory: keep numeric ids for persistence, and accept related public ids so mappers can populate domain public-id properties
    public static Order Rehydrate(
        string publicId,
        DateTime createdAt,
        OrderStatus status,
        OrderFulfillmentType fulfillmentType,
        OrderSource source,
        string? clientPublicId,
        string? clientGroupPublicId,
        string? locationPublicId,
        List<OrderLine> lines,
        List<OrderStatusHistory> statusHistory)
    {
        var order = new Order(
            publicId,
            createdAt,
            status,
            fulfillmentType,
            source,
            clientPublicId,
            clientGroupPublicId,
            locationPublicId);

        foreach (var line in lines)
        {
            order._lines.Add(line);
        }

        foreach (var history in statusHistory)
        {
            order._statusHistory.Add(history);
        }

        return order;
    }
}







