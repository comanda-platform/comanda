namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("Authorization")]
public class AuthorizationDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public int PersonId { get; set; }
    public virtual required PersonDatabaseEntity Person { get; set; }

    public int AccountId { get; set; }
    public virtual required AccountDatabaseEntity Account { get; set; }

    public int Role { get; set; }  // Maps to AuthorizationRole enum
    public bool IsActive { get; set; }
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
}
