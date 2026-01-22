using System.ComponentModel.DataAnnotations.Schema;

namespace Comanda.Database.Entities;

[Table("InventoryItem")]
public class InventoryItemDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required string Name { get; set; } // Rice, Beans, Oil, Chicken
    public DateTime CreatedAt { get; set; }

    // Other attributes
    public DateTime? LastModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    // Optional many-to-one relationships (audit)
    public int? CreatedById { get; set; }
    public virtual EmployeeDatabaseEntity? CreatedBy { get; set; }

    public int? LastModifiedById { get; set; }
    public virtual EmployeeDatabaseEntity? LastModifiedBy { get; set; }

    public int? DeletedById { get; set; }
    public virtual EmployeeDatabaseEntity? DeletedBy { get; set; }

    // Required many-to-one relationships
    public int BaseUnitId { get; set; }
    public virtual UnitDatabaseEntity BaseUnit { get; set; } = null!;

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
    public virtual ICollection<InventoryPurchaseLineDatabaseEntity> PurchaseLines { get; set; } = [];
    public virtual ICollection<RecipeIngredientDatabaseEntity> RecipeIngredients { get; set; } = [];
}







