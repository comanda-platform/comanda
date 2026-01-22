using System.ComponentModel.DataAnnotations.Schema;

namespace Comanda.Database.Entities;

[Table("Unit")]
public class UnitDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required string Code { get; set; } = null!; // "g", "kg", "ml", "l", "unit"
    public required string Name { get; set; } = null!; // "Gram", "Kilogram", "Milliliter", "Liter", "Unit"

    // Classification and conversion
    public int CategoryId { get; set; } // Weight, Volume, Count
    public decimal? ToBaseMultiplier { get; set; } // Conversion factor to base unit (e.g., 1000 for g→kg, 1 for base unit)

    // One-to-many relationships
    public virtual ICollection<InventoryItemDatabaseEntity> Items { get; set; } = [];
    public virtual ICollection<InventoryPurchaseLineDatabaseEntity> PurchaseLines { get; set; } = [];
    public virtual ICollection<RecipeIngredientDatabaseEntity> RecipeIngredients { get; set; } = [];
}






