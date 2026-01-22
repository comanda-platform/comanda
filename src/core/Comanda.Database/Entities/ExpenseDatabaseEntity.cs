using System.ComponentModel.DataAnnotations.Schema;

namespace Comanda.Database.Entities;

[Table("Expense")]
public class ExpenseDatabaseEntity
{
    // Identifiers
    public int Id { get; set; }
    public required string PublicId { get; set; }

    // Required attributes
    public required string Description { get; set; }  // "Senior Chef", "Rent", "Internet", etc
    public int ExpenseTypeId { get; set; }
    public decimal Amount { get; set; }
    public int Frequency { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }        // null = current/ongoing
    public DateTime CreatedAt { get; set; }

    // Payment schedule
    public int? DayOfMonth { get; set; }              // For monthly expenses: day 1-31 (null if not applicable)
    public DayOfWeek? DayOfWeek { get; set; }         // For weekly expenses: Monday-Sunday (null if not applicable)
    public DateTime? SpecificPayableDate { get; set; } // For one-time or specific date

    // For daily/per-diem calculations
    public int? DaysWorkedPerWeek { get; set; }       // e.g., 6 for typical employee (Amount is weekly)
    public bool CalculateDailyRate { get; set; }      // true: pay = Amount / DaysWorkedPerWeek

    // Other attributes
    public DateTime? LastModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    // Optional many-to-one relationships (audit)
    public int? CreatedById { get; set; }
    public virtual EmployeeDatabaseEntity? CreatedBy { get; set; }

    public int? LastModifiedById { get; set; }
    public virtual EmployeeDatabaseEntity? LastModifiedBy { get; set; }

    public int? DeletedById { get; set; }
    public virtual EmployeeDatabaseEntity? DeletedBy { get; set; }

    // Optional many-to-one relationships
    public int? LocationId { get; set; }              // null = applies to all locations for now, ready for multi-location
    public virtual LocationDatabaseEntity? Location { get; set; }

    public int? EmployeeId { get; set; }              // Only populated for EmployeeSalary type
    public virtual EmployeeDatabaseEntity? Employee { get; set; }

    // One-to-many relationships
    public virtual ICollection<NoteDatabaseEntity> Notes { get; set; } = [];
}







