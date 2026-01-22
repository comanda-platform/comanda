using System.ComponentModel.DataAnnotations.Schema;

namespace Comanda.Database.Entities;

[Table("DailyMenuItem")]
public class DailyMenuItemDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public string PublicId { get; set; }

    // Required attributes
    public int SequenceOrder { get; set; }  // Display order on menu

    // Other attributes
    public string? OverriddenName { get; set; }
    public decimal? OverriddenPrice { get; set; }
    public DateTime CreatedAt { get; set; }
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
    public int DailyMenuId { get; set; }
    public virtual required DailyMenuDatabaseEntity Menu { get; set; }

    public int ProductId { get; set; }
    public virtual required ProductDatabaseEntity Product { get; set; }

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
}







