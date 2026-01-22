namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("AuditLog")]
public class AuditLogDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }

    public required string EntityType { get; set; }
    public required int EntityId { get; set; }
    public required string Action { get; set; } // Created, Modified, Deleted
    public DateTime Timestamp { get; set; }

    public string? ChangedFields { get; set; } // JSON of changed fields with old/new values
    public string? OldValues { get; set; } // JSON snapshot of entity before change
    public string? NewValues { get; set; } // JSON snapshot of entity after change

    public int? ChangedById { get; set; }
    public virtual EmployeeDatabaseEntity? ChangedBy { get; set; }

    public int? ChangedByClientId { get; set; }
    public virtual ClientDatabaseEntity? ChangedByClient { get; set; }
}







