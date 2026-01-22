namespace Comanda.Domain.Entities;

using Comanda.Domain.Helpers;

public class Employee
{
    public string PublicId { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? ApiKey { get; private set; }
    public DateTime? ApiKeyCreatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Employee() { } // For reflection / serializers

    private Employee(
        string publicId,
        string userName,
        string email,
        string? phoneNumber,
        string? apiKey,
        DateTime? apiKeyCreated,
        bool isActive,
        DateTime createdAt)
    {
        PublicId = publicId;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        ApiKey = apiKey;
        ApiKeyCreatedAt = apiKeyCreated;
        IsActive = isActive;
        CreatedAt = createdAt;
    }

    public Employee(
        string userName,
        string email,
        string? phoneNumber = null)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("Username is required", nameof(userName));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));

        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format", nameof(email));

        PublicId = PublicIdHelper.Generate();
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(
        string userName,
        string email,
        string? phoneNumber)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(userName, "Username is required");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(email, "Email is required");

        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format", nameof(email));

        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void GenerateApiKey()
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot generate API key for inactive employee");

        ApiKey = GenerateSecureApiKey();
        ApiKeyCreatedAt = DateTime.UtcNow;
    }

    public void RevokeApiKey()
    {
        ApiKey = null;
        ApiKeyCreatedAt = null;
    }

    public bool ValidateApiKey(string providedKey)
    {
        if (string.IsNullOrEmpty(ApiKey))
            return false;

        if (!IsActive)
            return false;

        // Check if API key has expired (90 days)
        if (ApiKeyCreatedAt.HasValue && DateTime.UtcNow - ApiKeyCreatedAt.Value > TimeSpan.FromDays(90))
            return false;

        return ApiKey == providedKey;
    }

    public void Activate()
    {
        if (IsActive)
            throw new InvalidOperationException("Employee is already active");

        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("Employee is already inactive");

        IsActive = false;

        RevokeApiKey(); // Revoke API key when deactivating
    }

    private static string GenerateSecureApiKey()
    {
        // Generate a secure random API key
        var bytes = new byte[32];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();

        rng.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);

            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static Employee Rehydrate(
        string publicId,
        string userName,
        string email,
        string? phoneNumber,
        string? apiKey,
        DateTime? apiKeyCreated,
        bool isActive,
        DateTime createdAt)
    {
        return new Employee(
            publicId,
            userName,
            email,
            phoneNumber,
            apiKey,
            apiKeyCreated,
            isActive,
            createdAt);
    }
}






