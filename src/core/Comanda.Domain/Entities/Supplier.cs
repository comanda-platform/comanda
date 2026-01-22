namespace Comanda.Domain.Entities;

using Comanda.Shared.Enums;
using Comanda.Domain.Helpers;

public class Supplier
{
    public string PublicId { get; private set; }
    public string Name { get; private set; }
    public SupplierType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Supplier() { } // For reflection / serializers

    private Supplier(
        string publicId,
        string name,
        SupplierType type,
        DateTime createdAt)
    {
        PublicId = publicId;
        Name = name;
        Type = type;
        CreatedAt = createdAt;
    }

    public Supplier(
        string name,
        SupplierType type)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, "Supplier name is required");

        PublicId = PublicIdHelper.Generate();
        Name = name;
        Type = type;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, "Supplier name is required");

        Name = name;
    }

    public void UpdateType(SupplierType type)
    {
        Type = type;
    }

    public static Supplier Rehydrate(
        string publicId,
        string name,
        SupplierType type,
        DateTime createdAt)
        => new(
            publicId,
            name,
            type,
            createdAt);
}







