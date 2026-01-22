namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("InventoryPurchaseLine")]
public class InventoryPurchaseLineDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
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
    public int InventoryPurchaseId { get; set; }
    public virtual InventoryPurchaseDatabaseEntity Purchase { get; set; } = null!;

    public int InventoryItemId { get; set; }
    public virtual InventoryItemDatabaseEntity Item { get; set; } = null!;

    public int UnitId { get; set; }
    public virtual UnitDatabaseEntity Unit { get; set; } = null!;

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
}







