namespace Comanda.Domain.Entities;

public class OrderLineSide
{
    public string OrderLinePublicId { get; private set; }
    public Side Side { get; private set; }

    private OrderLineSide() { } // For reflection / serializers

    // DB-compatible
    private OrderLineSide(
        string orderLinePublicId,
        Side side)
    {
        OrderLinePublicId = orderLinePublicId;
        Side = side;
    }

    // Domain constructor using public id
    public OrderLineSide(Side side)
    {
        ArgumentNullException.ThrowIfNull(side);

        Side = side;
    }

    internal void SetOrderLinePublicId(string orderLinePublicId)
    {
        OrderLinePublicId = orderLinePublicId;
    }

    public static OrderLineSide Rehydrate(
        string orderLinePublicId,
        Side side)
        => new(
            orderLinePublicId,
            side);
}







