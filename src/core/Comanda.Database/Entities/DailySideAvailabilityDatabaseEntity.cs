using System.ComponentModel.DataAnnotations.Schema;

namespace Comanda.Database.Entities;

[Table("DailySideAvailability")]
public class DailySideAvailabilityDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public DateOnly Date { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }

    // Other attributes
    public DateTime? LastModifiedAt { get; set; }

    // Optional many-to-one relationships (audit)
    public int? CreatedById { get; set; }
    public virtual EmployeeDatabaseEntity? CreatedBy { get; set; }

    public int? LastModifiedById { get; set; }
    public virtual EmployeeDatabaseEntity? LastModifiedBy { get; set; }

    // Required many-to-one relationships
    public int SideId { get; set; }
    public virtual SideDatabaseEntity Side { get; set; } = null!;
}







