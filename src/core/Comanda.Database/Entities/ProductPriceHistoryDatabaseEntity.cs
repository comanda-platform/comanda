namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("ProductPriceHistory")]
public class ProductPriceHistoryDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public string PublicId { get; set; }

    // Required attributes
    public decimal Price { get; set; }
    public DateTime EffectiveFrom { get; set; }

    // Other attributes
    public DateTime? EffectiveTo { get; set; }

    // Optional one-to-one relationships
    public int? CreatedById { get; set; }
    public virtual EmployeeDatabaseEntity? CreatedBy { get; set; }

    // Required many-to-one relationships
    public int ProductId { get; set; }
    public virtual required ProductDatabaseEntity Product { get; set; }

    // One-to-many relationships (Reason is now a Note)
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
}







