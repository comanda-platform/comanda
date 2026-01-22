using Comanda.Domain.Helpers;

namespace Comanda.Domain.Entities;

public class Account
{
    public string PublicId { get; private set; }
    public string Name { get; private set; }
    public bool HasCreditLine { get; private set; }
    public decimal? CreditLimit { get; private set; }

    private readonly List<Location> _locations = [];
    public IReadOnlyCollection<Location> Locations => _locations.AsReadOnly();

    private Account()
    {
        // For EF Core
        PublicId = string.Empty;
        Name = string.Empty;
    }

    private Account(
        string publicId,
        string name,
        bool hasCreditLine,
        decimal? creditLimit)
    {
        PublicId = publicId;
        Name = name;
        HasCreditLine = hasCreditLine;
        CreditLimit = creditLimit;
    }

    public Account(
        string name,
        bool hasCreditLine = false,
        decimal? creditLimit = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Account name is required", nameof(name));

        if (hasCreditLine && creditLimit.HasValue && creditLimit.Value <= 0)
            throw new ArgumentException("Credit limit must be positive", nameof(creditLimit));

        PublicId = PublicIdHelper.Generate();
        Name = name;
        HasCreditLine = hasCreditLine;
        CreditLimit = creditLimit;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Account name is required", nameof(name));

        Name = name;
    }

    public void EnableCreditLine(decimal? creditLimit = null)
    {
        if (HasCreditLine)
            throw new InvalidOperationException("Credit line is already enabled");

        if (creditLimit.HasValue && creditLimit.Value <= 0)
            throw new ArgumentException("Credit limit must be positive", nameof(creditLimit));

        HasCreditLine = true;
        CreditLimit = creditLimit;
    }

    public void DisableCreditLine()
    {
        if (!HasCreditLine)
            throw new InvalidOperationException("Credit line is already disabled");

        HasCreditLine = false;
        CreditLimit = null;
    }

    public void UpdateCreditLimit(decimal? creditLimit)
    {
        if (!HasCreditLine)
            throw new InvalidOperationException("Cannot update credit limit when credit line is disabled");

        if (creditLimit.HasValue && creditLimit.Value <= 0)
            throw new ArgumentException("Credit limit must be positive", nameof(creditLimit));

        CreditLimit = creditLimit;
    }

    public static Account Rehydrate(
        string publicId,
        string name,
        bool hasCreditLine,
        decimal? creditLimit,
        List<Location> locations)
    {
        var account = new Account(publicId, name, hasCreditLine, creditLimit);

        foreach (var location in locations)
            account._locations.Add(location);

        return account;
    }
}
