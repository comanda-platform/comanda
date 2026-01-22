using Comanda.Shared.Enums;

namespace Comanda.Client.Admin.Models;

public record ProductionBatchResponse(
    string PublicId,
    string ProductPublicId,
    string DailyMenuPublicId,
    DateOnly ProductionDate,
    BatchStatus Status,
    DateTime StartedAt,
    DateTime? CompletedAt,
    int? Yield,
    string? StartedByPublicId,
    string? CompletedByPublicId,
    string? Notes);

public record CreateProductionBatchRequest(
    string ProductPublicId,
    string DailyMenuPublicId,
    DateOnly ProductionDate,
    string? StartedByPublicId);

public record CompleteBatchRequest(
    int Yield,
    string? CompletedByPublicId,
    string? Notes);

public record UpdateBatchNotesRequest(
    string? Notes);










