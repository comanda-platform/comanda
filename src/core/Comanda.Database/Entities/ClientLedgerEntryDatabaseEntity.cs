namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("ClientLedgerEntry")]
public class ClientLedgerEntryDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    public decimal Amount { get; set; }
    public int LedgerEntryTypeId { get; set; }
    public DateTime OccurredAt { get; set; }
    public DateTime CreatedAt { get; set; }

    // Other attributes
    public int? PaymentMethodId { get; set; }
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

    // Required many-to-one relationships
    public int ClientId { get; set; }
    public virtual required ClientDatabaseEntity Client { get; set; }

    // Optional many-to-one relationships
    public int? OrderLineId { get; set; }
    public virtual OrderLineDatabaseEntity? OrderLine { get; set; }

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
}







