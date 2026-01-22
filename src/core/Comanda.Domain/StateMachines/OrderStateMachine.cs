namespace Comanda.Domain.StateMachines;

using Comanda.Shared.Enums;

public static class OrderStateMachine
{
    private static readonly Dictionary<OrderStatus, HashSet<OrderStatus>> AllowedTransitions = new()
    {
        [OrderStatus.Unknown] = new HashSet<OrderStatus> { OrderStatus.Created },
        [OrderStatus.Created] = new HashSet<OrderStatus> { OrderStatus.Accepted, OrderStatus.Cancelled },
        [OrderStatus.Accepted] = new HashSet<OrderStatus> { OrderStatus.Preparing, OrderStatus.Cancelled },
        [OrderStatus.Preparing] = new HashSet<OrderStatus> { OrderStatus.Ready, OrderStatus.Cancelled },
        [OrderStatus.Ready] = new HashSet<OrderStatus> { OrderStatus.InTransit, OrderStatus.Delivered, OrderStatus.Completed, OrderStatus.Cancelled },
        [OrderStatus.InTransit] = new HashSet<OrderStatus> { OrderStatus.Delivered, OrderStatus.Cancelled },
        [OrderStatus.Delivered] = new HashSet<OrderStatus> { OrderStatus.Completed },
        [OrderStatus.Completed] = new HashSet<OrderStatus>(),
        [OrderStatus.Cancelled] = new HashSet<OrderStatus>()
    };

    private static readonly Dictionary<OrderStatus, string> StatusDescriptions = new()
    {
        [OrderStatus.Unknown] = "Status unknown",
        [OrderStatus.Created] = "Order placed, waiting for acceptance",
        [OrderStatus.Accepted] = "Order accepted by kitchen",
        [OrderStatus.Preparing] = "Kitchen is preparing the order",
        [OrderStatus.Ready] = "Order ready for pickup/delivery/serving",
        [OrderStatus.InTransit] = "Order is being delivered",
        [OrderStatus.Delivered] = "Order has been delivered",
        [OrderStatus.Completed] = "Order completed successfully",
        [OrderStatus.Cancelled] = "Order was cancelled"
    };

    private static readonly HashSet<OrderStatus> CancellableStatuses = new()
    {
        OrderStatus.Created,
        OrderStatus.Accepted,
        OrderStatus.Preparing,
        OrderStatus.Ready,
        OrderStatus.InTransit
    };

    private static readonly HashSet<OrderStatus> ActiveStatuses = new()
    {
        OrderStatus.Created,
        OrderStatus.Accepted,
        OrderStatus.Preparing,
        OrderStatus.Ready,
        OrderStatus.InTransit
    };

    public static bool CanTransitionTo(OrderStatus from, OrderStatus to)
    {
        if (!AllowedTransitions.TryGetValue(from, out var allowedStates))
            return false;

        return allowedStates.Contains(to);
    }

    public static IEnumerable<OrderStatus> GetAllowedTransitions(OrderStatus from)
    {
        return AllowedTransitions.TryGetValue(from, out var allowedStates)
            ? allowedStates
            : Enumerable.Empty<OrderStatus>();
    }

    public static string GetDescription(OrderStatus status)
    {
        return StatusDescriptions.TryGetValue(status, out var description)
            ? description
            : "Unknown status";
    }

    public static bool CanBeCancelled(OrderStatus status)
    {
        return CancellableStatuses.Contains(status);
    }

    public static bool IsActiveStatus(OrderStatus status)
    {
        return ActiveStatuses.Contains(status);
    }

    public static bool IsTerminalStatus(OrderStatus status)
    {
        return status == OrderStatus.Completed || status == OrderStatus.Cancelled;
    }

    public static bool RequiresDelivery(OrderStatus status)
    {
        return status == OrderStatus.InTransit;
    }

    public static IEnumerable<OrderStatus> GetActiveStatuses() => ActiveStatuses;

    public static IEnumerable<OrderStatus> GetAllStatuses() => Enum.GetValues<OrderStatus>();
}







