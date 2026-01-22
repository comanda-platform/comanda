namespace Comanda.Domain.Entities;

using Comanda.Shared.Enums;
using Comanda.Domain.Helpers;

public class LedgerEntry
{
    public string PublicId { get; private set; }
    public string ClientPublicId { get; private set; }
    public DateTime OccurredAt { get; private set; }

    /// <summary>
    /// Positive = debt (credit to client account)
    /// Negative = payment (debit from client account)
    /// </summary>
    public decimal Amount { get; private set; }

    public LedgerEntryType Type { get; private set; }
    public PaymentMethod? PaymentMethod { get; private set; }
    public string? OrderLinePublicId { get; private set; }

    private LedgerEntry() { } // For reflection / serializers

    private LedgerEntry(
        string publicId,
        string clientPublicId,
        DateTime occurredAt,
        decimal amount,
        LedgerEntryType type,
        PaymentMethod? paymentMethod,
        string? orderLinePublicId)
    {
        PublicId = publicId;
        ClientPublicId = clientPublicId;
        OccurredAt = occurredAt;
        Amount = amount;
        Type = type;
        PaymentMethod = paymentMethod;
        OrderLinePublicId = orderLinePublicId;
    }

    // Domain create methods using public ids
    public static LedgerEntry CreateCredit(
        string clientPublicId,
        decimal amount,
        string? orderLinePublicId = null)
    {
        if (amount <= 0)
            throw new ArgumentException("Credit amount must be positive", nameof(amount));

        return new LedgerEntry
        {
            PublicId = PublicIdHelper.Generate(),
            ClientPublicId = clientPublicId,
            OccurredAt = DateTime.UtcNow,
            Amount = amount,
            Type = LedgerEntryType.Credit,
            OrderLinePublicId = orderLinePublicId,
        };
    }

    public static LedgerEntry CreatePayment(
        string clientPublicId,
        decimal amount,
        PaymentMethod paymentMethod)
    {
        if (amount <= 0)
            throw new ArgumentException("Payment amount must be positive", nameof(amount));

        return new LedgerEntry
        {
            PublicId = PublicIdHelper.Generate(),
            ClientPublicId = clientPublicId,
            OccurredAt = DateTime.UtcNow,
            Amount = -amount, // Negative because it reduces debt
            Type = LedgerEntryType.Payment,
            PaymentMethod = paymentMethod
        };
    }

    public static LedgerEntry CreateAdjustment(
        string clientPublicId,
        decimal amount)
    {
        return new LedgerEntry
        {
            PublicId = PublicIdHelper.Generate(),
            ClientPublicId = clientPublicId,
            OccurredAt = DateTime.UtcNow,
            Amount = amount, // Can be positive or negative
            Type = LedgerEntryType.Adjustment
        };
    }

    public static LedgerEntry CreateWriteOff(
        string clientPublicId,
        decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Write-off amount must be positive", nameof(amount));

        return new LedgerEntry
        {
            PublicId = PublicIdHelper.Generate(),
            ClientPublicId = clientPublicId,
            OccurredAt = DateTime.UtcNow,
            Amount = -amount, // Negative because it reduces debt
            Type = LedgerEntryType.WriteOff
        };
    }

    public bool IsDebit => Amount < 0;
    public bool IsCredit => Amount > 0;
    public decimal AbsoluteAmount => Math.Abs(Amount);

    public static LedgerEntry Rehydrate(
        string publicId,
        string clientPublicId,
        DateTime occurredAt,
        decimal amount,
        LedgerEntryType type,
        PaymentMethod? paymentMethod,
        string? orderLinePublicId)
        => new(
            publicId,
            clientPublicId,
            occurredAt,
            amount,
            type,
            paymentMethod,
            orderLinePublicId);
}







