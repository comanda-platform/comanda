using System.ComponentModel.DataAnnotations.Schema;

namespace Comanda.Database.Entities;

[Table("OrderLineOption")]
public class OrderLineOptionDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required string OptionKey { get; set; } // Examples: "Water", "PlasticUtensils", "Container", "Salsa"
    public DateTime CreatedAt { get; set; }

    // Other attributes
    public DateTime? LastModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsIncluded { get; set; }

    // Optional one-to-one relationships
    public int? CreatedById { get; set; }
    public virtual EmployeeDatabaseEntity? CreatedBy { get; set; }

    public int? LastModifiedById { get; set; }
    public virtual EmployeeDatabaseEntity? LastModifiedBy { get; set; }

    public int? DeletedById { get; set; }
    public virtual EmployeeDatabaseEntity? DeletedBy { get; set; }

    // Many-to-one relationships
    public int OrderLineId { get; set; }
    public virtual OrderLineDatabaseEntity OrderLine { get; set; } = null!;
}







