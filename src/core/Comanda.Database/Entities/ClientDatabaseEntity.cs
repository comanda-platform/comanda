namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("Client")]
public class ClientDatabaseEntity
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

    // Optional many-to-one relationships
    public int? CreatedById { get; set; }
    public virtual EmployeeDatabaseEntity? CreatedBy { get; set; }

    public int? LastModifiedById { get; set; }
    public virtual EmployeeDatabaseEntity? LastModifiedBy { get; set; }

    public int? DeletedById { get; set; }
    public virtual EmployeeDatabaseEntity? DeletedBy { get; set; }

    public int? ClientGroupId { get; set; }
    public virtual ClientGroupDatabaseEntity? Group { get; set; }

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
    public virtual ICollection<OrderLineDatabaseEntity> OrderLines { get; set; } = [];
    public virtual ICollection<ClientLedgerEntryDatabaseEntity> LedgerEntries { get; set; } = [];
    public virtual ICollection<LocationDatabaseEntity> Locations { get; set; } = [];
    public virtual ICollection<ClientContactDatabaseEntity> Contacts { get; set; } = [];
}







