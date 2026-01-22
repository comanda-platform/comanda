namespace Comanda.Domain.Entities;

using Comanda.Shared.Enums;
using Comanda.Domain.Helpers;

public class Unit
{
    public string PublicId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public UnitCategory Category { get; private set; }
    public decimal? ToBaseMultiplier { get; private set; }

    private Unit() { } // For reflection / serializers

    private Unit(
        string publicId,
        string code,
        string name,
        UnitCategory category,
        decimal? toBaseMultiplier)
    {
        PublicId = publicId;
        Code = code;
        Name = name;
        Category = category;
        ToBaseMultiplier = toBaseMultiplier;
    }

    public Unit(
        string code,
        string name,
        UnitCategory category,
        decimal? toBaseMultiplier = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code, "Unit code is required");
        ArgumentException.ThrowIfNullOrWhiteSpace(name, "Unit name is required");
        
        PublicId = PublicIdHelper.Generate();
        Code = code;
        Name = name;
        Category = category;
        ToBaseMultiplier = toBaseMultiplier;
    }

    public void UpdateDetails(
        string code,
        string name,
        UnitCategory category,
        decimal? toBaseMultiplier)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code, "Unit code is required");
        ArgumentException.ThrowIfNullOrWhiteSpace(name, "Unit name is required");

        Code = code;
        Name = name;
        Category = category;
        ToBaseMultiplier = toBaseMultiplier;
    }

    public decimal ConvertToBase(decimal quantity)
    {
        if (!ToBaseMultiplier.HasValue)
            return quantity;

        return quantity * ToBaseMultiplier.Value;
    }

    public decimal ConvertFromBase(decimal quantity)
    {
        if (!ToBaseMultiplier.HasValue || ToBaseMultiplier.Value == 0)
            return quantity;

        return quantity / ToBaseMultiplier.Value;
    }

    public static Unit Rehydrate(
        string publicId,
        string code,
        string name,
        UnitCategory category,
        decimal? toBaseMultiplier)
        => new(
            publicId,
            code,
            name,
            category,
            toBaseMultiplier);
}







