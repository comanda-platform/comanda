using System.ComponentModel.DataAnnotations.Schema;

namespace Comanda.Database.Entities;

[Table("Side")]
public class SideDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }

    // Other attributes
    public bool IsActive { get; set; } = true;
    public DateTime? LastModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    // Optional many-to-one relationships
    public int? CreatedById { get; set; }
    public virtual EmployeeDatabaseEntity? CreatedBy { get; set; }

    public int? LastModifiedById { get; set; }
    public virtual EmployeeDatabaseEntity? LastModifiedBy { get; set; }

    public int? DeletedById { get; set; }
    public virtual EmployeeDatabaseEntity? DeletedBy { get; set; }

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
    public virtual ICollection<DailySideAvailabilityDatabaseEntity> DailyAvailabilities { get; set; } = [];
    public virtual ICollection<OrderLineSideDatabaseEntity> OrderLineSides { get; set; } = [];
}







