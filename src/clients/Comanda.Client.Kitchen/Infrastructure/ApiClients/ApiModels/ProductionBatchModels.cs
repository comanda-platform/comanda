namespace Comanda.Client.Kitchen.Infrastructure.ApiClients.ApiModels;

using Comanda.Shared.Enums;

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

public record StartBatchRequest(
    string ProductPublicId,
    string DailyMenuPublicId,
    DateOnly ProductionDate,
    string? StartedByPublicId = null);

public record CompleteBatchRequest(
    int Yield,
    string? CompletedByPublicId = null,
    string? Notes = null);

public record TotalYieldResponse(
    string ProductPublicId,
    string Date,
    int TotalYield);







