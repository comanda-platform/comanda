namespace Comanda.Database.Entities;

using Microsoft.AspNetCore.Identity;

public class EmployeeDatabaseEntity : IdentityUser<int>
{
    // Identifiers
    // (Id inherited from IdentityUser<int> as int)
    public required string PublicId { get; set; }

    // Required attributes
    public DateTime CreatedAt { get; set; }

    // Other attributes
    public string? ApiKey { get; set; }
    public DateTime? ApiKeyCreatedAt { get; set; }
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
}







