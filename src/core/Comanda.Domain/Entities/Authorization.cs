using Comanda.Domain.Helpers;
using Comanda.Shared.Enums;

namespace Comanda.Domain.Entities;

public class Authorization
{
    public string PublicId { get; private set; }
    public string PersonPublicId { get; private set; }
    public string AccountPublicId { get; private set; }
    public AuthorizationRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Authorization()
    {
        // For EF Core
        PublicId = string.Empty;
        PersonPublicId = string.Empty;
        AccountPublicId = string.Empty;
    }

    private Authorization(
        string publicId,
        string personPublicId,
        string accountPublicId,
        AuthorizationRole role,
        bool isActive,
        DateTime createdAt)
    {
        PublicId = publicId;
        PersonPublicId = personPublicId;
        AccountPublicId = accountPublicId;
        Role = role;
        IsActive = isActive;
        CreatedAt = createdAt;
    }

    public Authorization(
        string personPublicId,
        string accountPublicId,
        AuthorizationRole role = AuthorizationRole.Orderer)
    {
        if (string.IsNullOrWhiteSpace(personPublicId))
            throw new ArgumentException("Person is required", nameof(personPublicId));

        if (string.IsNullOrWhiteSpace(accountPublicId))
            throw new ArgumentException("Account is required", nameof(accountPublicId));

        PublicId = PublicIdHelper.Generate();
        PersonPublicId = personPublicId;
        AccountPublicId = accountPublicId;
        Role = role;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateRole(AuthorizationRole newRole)
    {
        Role = newRole;
    }

    public void Activate()
    {
        if (IsActive)
            throw new InvalidOperationException("Authorization is already active");

        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("Authorization is already inactive");

        IsActive = false;
    }

    public static Authorization Rehydrate(
        string publicId,
        string personPublicId,
        string accountPublicId,
        AuthorizationRole role,
        bool isActive,
        DateTime createdAt)
        => new(publicId, personPublicId, accountPublicId, role, isActive, createdAt);
}
