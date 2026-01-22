using System.ComponentModel.DataAnnotations.Schema;

namespace Comanda.Database.Entities;

[Table("DailyMenu")]
public class DailyMenuDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public DateOnly Date { get; set; }
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

    // Optional many-to-one relationships
    public int LocationId { get; set; } 
    public virtual LocationDatabaseEntity Location { get; set; }

    // One-to-many relationships
    public virtual ICollection<DailyMenuItemDatabaseEntity> Items { get; set; } = [];
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
}







