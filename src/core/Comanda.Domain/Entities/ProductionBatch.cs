namespace Comanda.Domain.Entities;

using Comanda.Shared.Enums;
using Comanda.Domain.Helpers;

/// <summary>
/// Represents a batch of food being cooked in the kitchen.
/// Tracks cooking status, start/end times, and yield (portions produced).
/// </summary>
public class ProductionBatch
{
    public string PublicId { get; private set; }
    public string ProductPublicId { get; private set; }
    public string DailyMenuPublicId { get; private set; }
    public DateOnly ProductionDate { get; private set; }
    public BatchStatus Status { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public int? Yield { get; private set; }
    public string? StartedByPublicId { get; private set; }
    public string? CompletedByPublicId { get; private set; }
    public string? Notes { get; private set; }

    private ProductionBatch() { } // For reflection / serializers

    // DB-compatible constructor
    private ProductionBatch(
        string publicId,
        string productPublicId,
        string dailyMenuPublicId,
        DateOnly productionDate,
        BatchStatus status,
        DateTime startedAt,
        DateTime? completedAt,
        int? yield,
        string? startedByPublicId,
        string? completedByPublicId,
        string? notes)
    {
        PublicId = publicId;
        ProductPublicId = productPublicId;
        DailyMenuPublicId = dailyMenuPublicId;
        ProductionDate = productionDate;
        Status = status;
        StartedAt = startedAt;
        CompletedAt = completedAt;
        Yield = yield;
        StartedByPublicId = startedByPublicId;
        CompletedByPublicId = completedByPublicId;
        Notes = notes;
    }

    /// <summary>
    /// Start a new production batch for a product
    /// </summary>
    public ProductionBatch(
        string productPublicId,
        string dailyMenuPublicId,
        DateOnly productionDate,
        string? startedByPublicId = null)
    {
        if (string.IsNullOrWhiteSpace(productPublicId))
            throw new ArgumentException("Product public ID is required", nameof(productPublicId));
        if (string.IsNullOrWhiteSpace(dailyMenuPublicId))
            throw new ArgumentException("Daily menu public ID is required", nameof(dailyMenuPublicId));

        PublicId = PublicIdHelper.Generate();
        ProductPublicId = productPublicId;
        DailyMenuPublicId = dailyMenuPublicId;
        ProductionDate = productionDate;
        Status = BatchStatus.InProgress;
        StartedAt = DateTime.UtcNow;
        StartedByPublicId = startedByPublicId;
    }

    /// <summary>
    /// Complete the batch with the yield (number of portions produced)
    /// </summary>
    public void Complete(int yield, string? completedByPublicId = null, string? notes = null)
    {
        if (Status != BatchStatus.InProgress)
            throw new InvalidOperationException($"Cannot complete batch in status {Status}");

        if (yield < 0)
            throw new ArgumentException("Yield cannot be negative", nameof(yield));

        Status = BatchStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        Yield = yield;
        CompletedByPublicId = completedByPublicId;
        Notes = notes;
    }

    /// <summary>
    /// Update notes on the batch
    /// </summary>
    public void UpdateNotes(string? notes)
    {
        Notes = notes;
    }

    public TimeSpan? CookingDuration => CompletedAt.HasValue
        ? CompletedAt.Value - StartedAt
        : null;

    public bool IsCompleted => Status == BatchStatus.Completed;

    /// <summary>
    /// Rehydrate from database
    /// </summary>
    public static ProductionBatch Rehydrate(
        string publicId,
        string productPublicId,
        string dailyMenuPublicId,
        DateOnly productionDate,
        BatchStatus status,
        DateTime startedAt,
        DateTime? completedAt,
        int? yield,
        string? startedByPublicId,
        string? completedByPublicId,
        string? notes)
    {
        return new ProductionBatch(
            publicId,
            productPublicId,
            dailyMenuPublicId,
            productionDate,
            status,
            startedAt,
            completedAt,
            yield,
            startedByPublicId,
            completedByPublicId,
            notes);
    }
}







