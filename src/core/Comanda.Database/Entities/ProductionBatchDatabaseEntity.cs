namespace Comanda.Database.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Comanda.Shared.Enums;

[Table("ProductionBatch")]
public class ProductionBatchDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required int ProductId { get; set; }
    public required int DailyMenuId { get; set; }
    public required DateOnly ProductionDate { get; set; }
    public required BatchStatus Status { get; set; }
    public required DateTime StartedAt { get; set; }

    // Optional attributes
    public DateTime? CompletedAt { get; set; }
    public int? Yield { get; set; }
    public string? Notes { get; set; }

    // Optional relationships
    public int? StartedById { get; set; }
    public virtual EmployeeDatabaseEntity? StartedBy { get; set; }

    public int? CompletedById { get; set; }
    public virtual EmployeeDatabaseEntity? CompletedBy { get; set; }

    // Required relationships
    public virtual ProductDatabaseEntity Product { get; set; } = null!;
    public virtual DailyMenuDatabaseEntity DailyMenu { get; set; } = null!;
}







