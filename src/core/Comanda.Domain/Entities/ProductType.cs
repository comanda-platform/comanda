namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;

public class ProductType
{
    public string PublicId { get; private set; }
    public string Name { get; private set; }

    private ProductType() { } // For reflection / serializers

    private ProductType(
        string publicId,
        string name)
    {
        PublicId = publicId;
        Name = name;
    }

    public ProductType(string name)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name, "Product type name is required");

        PublicId = PublicIdHelper.Generate();
        Name = name;
    }

    public static ProductType Rehydrate(
        string publicId,
        string name) 
        => new (
            publicId,
            name);
}






