namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("ClientGroup")]
public class ClientGroupDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required string Name { get; set; }
    public bool HasCreditLine { get; set; }
    public DateTime CreatedAt { get; set; }

    // Other attributes
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

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
    public virtual ICollection<ClientDatabaseEntity> Members { get; set; } = [];
    public virtual ICollection<OrderDatabaseEntity> Orders { get; set; } = [];
    public virtual ICollection<LocationDatabaseEntity> Locations { get; set; } = [];
}







