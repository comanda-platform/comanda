namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("PersonContact")]
public class PersonContactDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required int Type { get; set; }  // Maps to ClientContactType enum
    public required string Value { get; set; }
    public DateTime CreatedAt { get; set; }

    // Other attributes
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }

    // Optional many-to-one relationships (audit trail)
    public int? CreatedById { get; set; }
    public virtual EmployeeDatabaseEntity? CreatedBy { get; set; }

    public int? LastModifiedById { get; set; }
    public virtual EmployeeDatabaseEntity? LastModifiedBy { get; set; }

    public int? DeletedById { get; set; }
    public virtual EmployeeDatabaseEntity? DeletedBy { get; set; }

    // Required many-to-one relationship to Person
    public int PersonId { get; set; }
    public virtual required PersonDatabaseEntity Person { get; set; }
}
