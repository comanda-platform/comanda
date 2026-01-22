using System.ComponentModel.DataAnnotations.Schema;

namespace Comanda.Database.Entities;

[Table("InventoryPurchase")]
public class InventoryPurchaseDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public DateTime PurchasedAt { get; set; }
    public required decimal TotalAmount { get; set; }
    public int PurchaseTypeId { get; set; }
    public DateTime CreatedAt { get; set; }

    // Other attributes
    public DateTime? DeliveredAt { get; set; }  // When it was received (null if in-store purchase)
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
    public int SupplierId { get; set; }
    public virtual SupplierDatabaseEntity Supplier { get; set; } = null!;

    // Optional many-to-one relationships
    public int? StoreLocationId { get; set; }  // Where we received/bought (null for online purchases like Amazon)
    public virtual LocationDatabaseEntity? StoreLocation { get; set; }

    // One-to-many relationships
    public virtual ICollection<InventoryPurchaseLineDatabaseEntity> Lines { get; set; } = [];
}







