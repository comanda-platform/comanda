namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("Order")]
public class OrderDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required int OrderStatusTypeId { get; set; }
    public required int FulfillmentTypeId { get; set; }
    public required int OrderSourceId { get; set; }
    public required DateTime CreatedAt { get; set; }

    // Other attributes
    public DateTime? LastModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    // Optional one-to-one relationships
    public int? CreatedById { get; set; }
    public virtual EmployeeDatabaseEntity? CreatedBy { get; set; }

    public int? CreatedByClientId { get; set; }
    public virtual ClientDatabaseEntity? CreatedByClient { get; set; }

    public int? LastModifiedById { get; set; }
    public virtual EmployeeDatabaseEntity? LastModifiedBy { get; set; }

    public int? DeletedById { get; set; }
    public virtual EmployeeDatabaseEntity? DeletedBy { get; set; }

    // Optional many-to-one relationships
    public int? ClientId { get; set; }
    public virtual ClientDatabaseEntity? Client { get; set; }

    public int? ClientGroupId { get; set; }
    public virtual ClientGroupDatabaseEntity? ClientGroup { get; set; }

    public int? LocationId { get; set; }
    public virtual LocationDatabaseEntity? Location { get; set; }

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
    public virtual ICollection<OrderLineDatabaseEntity> Lines { get; set; } = [];
    public virtual ICollection<OrderStatusHistoryDatabaseEntity> StatusHistory { get; set; } = [];
}







