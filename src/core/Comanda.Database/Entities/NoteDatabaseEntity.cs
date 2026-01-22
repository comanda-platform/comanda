namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("Note")]
public class NoteDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    // Optional many-to-one relationships
    public int? CreatedById { get; set; }
    public virtual EmployeeDatabaseEntity? CreatedBy { get; set; }

    // Optional many-to-one relationships (polymorphic - only one should be set)
    public int? ClientId { get; set; }
    public virtual ClientDatabaseEntity? Client { get; set; }

    public int? ClientGroupId { get; set; }
    public virtual ClientGroupDatabaseEntity? ClientGroup { get; set; }

    public int? LocationId { get; set; }
    public virtual LocationDatabaseEntity? Location { get; set; }

    public int? OrderId { get; set; }
    public virtual OrderDatabaseEntity? Order { get; set; }

    public int? OrderLineId { get; set; }
    public virtual OrderLineDatabaseEntity? OrderLine { get; set; }

    public int? OrderStatusHistoryId { get; set; }
    public virtual OrderStatusHistoryDatabaseEntity? OrderStatusHistory { get; set; }

    public int? ProductId { get; set; }
    public virtual ProductDatabaseEntity? Product { get; set; }

    public int? ProductPriceHistoryId { get; set; }
    public virtual ProductPriceHistoryDatabaseEntity? ProductPriceHistory { get; set; }

    public int? ClientLedgerEntryId { get; set; }
    public virtual ClientLedgerEntryDatabaseEntity? ClientLedgerEntry { get; set; }

    public int? SideId { get; set; }
    public virtual SideDatabaseEntity? Side { get; set; }
}







