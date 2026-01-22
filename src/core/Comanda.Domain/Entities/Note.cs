namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;

public class Note
{
    public string PublicId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string? CreatedByPublicId { get; private set; }

    // Polymorphic associations - only one should be set
    public string? ClientPublicId { get; private set; }
    public string? ClientGroupPublicId { get; private set; }
    public string? LocationPublicId { get; private set; }
    public string? OrderPublicId { get; private set; }
    public string? OrderLinePublicId { get; private set; }
    public string? ProductPublicId { get; private set; }
    public string? SidePublicId { get; private set; }

    private Note() { } // For reflection / serializers

    // Database-compatible constructor (kept for mappers)
    private Note(
        string publicId,
        string content,
        DateTime createdAt,
        string? createdByPublicId,
        string? clientPublicId,
        string? clientGroupPublicId,
        string? locationPublicId,
        string? orderPublicId,
        string? orderLinePublicId,
        string? productPublicId,
        string? sidePublicId)
    {
        PublicId = publicId;
        Content = content;
        CreatedAt = createdAt;
        CreatedByPublicId = createdByPublicId;
        ClientPublicId = clientPublicId;
        ClientGroupPublicId = clientGroupPublicId;
        LocationPublicId = locationPublicId;
        OrderPublicId = orderPublicId;
        OrderLinePublicId = orderLinePublicId;
        ProductPublicId = productPublicId;
        SidePublicId = sidePublicId;
    }

    // Public-id based internal constructor for domain usage
    private Note(
        string content,
        string? createdByPublicId = null)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(content, "Note content is required");

        PublicId = PublicIdHelper.Generate();
        Content = content;
        CreatedByPublicId = createdByPublicId;
        CreatedAt = DateTime.UtcNow;
    }

    // Factories that accept public ids for associations
    public static Note ForClient(
        string content,
        string clientPublicId,
        string? createdByPublicId = null)
    {
        var note = new Note(content, createdByPublicId)
        {
            ClientPublicId = clientPublicId
        };

        return note;
    }

    public static Note ForClientGroup(
        string content,
        string clientGroupPublicId,
        string? createdByPublicId = null)
    {
        var note = new Note(content, createdByPublicId)
        {
            ClientGroupPublicId = clientGroupPublicId
        };

        return note;
    }

    public static Note ForLocation(
        string content,
        string locationPublicId,
        string? createdByPublicId = null)
    {
        var note = new Note(content, createdByPublicId)
        {
            LocationPublicId = locationPublicId
        };

        return note;
    }

    public static Note ForOrder(
        string content,
        string orderPublicId,
        string? createdByPublicId = null)
    {
        var note = new Note(content, createdByPublicId)
        {
            OrderPublicId = orderPublicId
        };

        return note;
    }

    public static Note ForOrderLine(
        string content,
        string orderLinePublicId,
        string? createdByPublicId = null)
    {
        var note = new Note(content, createdByPublicId)
        {
            OrderLinePublicId = orderLinePublicId
        };

        return note;
    }

    public static Note ForProduct(
        string content,
        string productPublicId,
        string? createdByPublicId = null)
    {
        var note = new Note(content, createdByPublicId)
        {
            ProductPublicId = productPublicId
        };

        return note;
    }

    public static Note ForSide(
        string content,
        string sidePublicId,
        string? createdByPublicId = null)
    {
        var note = new Note(content, createdByPublicId)
        {
            SidePublicId = sidePublicId
        };

        return note;
    }

    // Keep existing DB-compatible factory for mappers
    public static Note Rehydrate(
        string publicId,
        string content,
        DateTime createdAt,
        string? createdByPublicId,
        string? clientPublicId,
        string? clientGroupPublicId,
        string? locationPublicId,
        string? orderPublicId,
        string? orderLinePublicId,
        string? productPublicId,
        string? sidePublicId)
        => new(
            publicId,
            content,
            createdAt,
            createdByPublicId,
            clientPublicId,
            clientGroupPublicId,
            locationPublicId,
            orderPublicId,
            orderLinePublicId,
            productPublicId,
            sidePublicId);
}







