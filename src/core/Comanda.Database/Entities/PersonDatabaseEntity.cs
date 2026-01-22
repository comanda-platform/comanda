namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("Person")]
public class PersonDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required string Name { get; set; }
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

    // One-to-many relationships
    public virtual ICollection<PersonContactDatabaseEntity> Contacts { get; set; } = [];
    public virtual ICollection<AuthorizationDatabaseEntity> Authorizations { get; set; } = [];
    public virtual ICollection<LocationDatabaseEntity> Locations { get; set; } = [];
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
}
