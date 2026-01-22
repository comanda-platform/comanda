namespace Comanda.Api.Models;

using Comanda.Shared.Enums;

public record CreateCreditEntryRequest(
    string ClientPublicId,
    decimal Amount,
    string OrderLinePublicId);

public record CreatePaymentEntryRequest(
    string ClientPublicId,
    decimal Amount,
    PaymentMethod PaymentMethod);

public record CreateAdjustmentEntryRequest(
    string ClientPublicId,
    decimal Amount);

public record CreateWriteOffEntryRequest(
    string ClientPublicId,
    decimal Amount);

/// <summary>
/// Unified request model for creating a ledger entry
/// </summary>
public record CreateLedgerEntryRequest
{
    public string EntryType { get; init; } = string.Empty; // credit, payment, adjustment, writeoff
    public string ClientPublicId { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string? OrderLinePublicId { get; init; }
    public string? PaymentMethod { get; init; }
}

public record LedgerEntryResponse(
    string PublicId,
    LedgerEntryType Type,
    decimal Amount,
    PaymentMethod? PaymentMethod,
    string ClientPublicId,
    string OrderLinePublicId,
    DateTime OccurredAt);







