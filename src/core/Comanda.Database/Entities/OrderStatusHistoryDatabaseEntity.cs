namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("OrderStatusHistory")]
public class OrderStatusHistoryDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public int OrderStatusTypeId { get; set; }
    public DateTime ChangedAt { get; set; }

    // Optional one-to-one relationships
    public int? ChangedById { get; set; }
    public virtual EmployeeDatabaseEntity? ChangedBy { get; set; }

    public int? ChangedByClientId { get; set; }
    public virtual ClientDatabaseEntity? ChangedByClient { get; set; }

    // Required many-to-one relationships
    public int OrderId { get; set; }
    public virtual required OrderDatabaseEntity Order { get; set; }

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
}







