namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Comanda.Shared.Enums;

[Table("OrderLine")]
public class OrderLineDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime CreatedAt { get; set; }

    // Prep tracking attributes
    public OrderLinePrepStatus PrepStatus { get; set; }
    public DateTime? PrepStartedAt { get; set; }
    public DateTime? PrepCompletedAt { get; set; }
    public string? ContainerType { get; set; }
    public string? SelectedSidesJson { get; set; }  // JSON array of side names

    // Other attributes
    public decimal? DiscountAmount { get; set; }      // Absolute discount ($5 off)
    public decimal? DiscountPercent { get; set; }     // Percentage discount (10% off)
    public int? DiscountReasonId { get; set; }        // Why discounted (promotion, bulk, error, etc)
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
    public int OrderId { get; set; }
    public virtual required OrderDatabaseEntity Order { get; set; }

    public int ProductId { get; set; }
    public virtual required ProductDatabaseEntity Product { get; set; }

    // Optional many-to-one relationships
    public int? ClientId { get; set; }
    public virtual ClientDatabaseEntity? Client { get; set; }

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
    public virtual ICollection<ClientLedgerEntryDatabaseEntity> LedgerEntries { get; set; } = [];
}







