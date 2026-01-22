namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("Product")]
public class ProductDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }

    // Other attributes
    public string? Description { get; set; }
    public bool IsSuggestedForDailyMenu { get; set; } = false;
    public DateTime? LastModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    // Optional one-to-one relationships
    public int? CreatedById { get; set; }
    public virtual EmployeeDatabaseEntity? CreatedBy { get; set; }

    public int? LastModifiedById { get; set; }
    public virtual EmployeeDatabaseEntity? LastModifiedBy { get; set; }

    public int? DeletedById { get; set; }
    public virtual EmployeeDatabaseEntity? DeletedBy { get; set; }

    // Required many-to-one relationships
    public int ProductTypeId { get; set; }
    public virtual required ProductTypeDatabaseEntity Type { get; set; }

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
    public virtual ICollection<ProductPriceHistoryDatabaseEntity> PriceHistory { get; set; } = [];
}







