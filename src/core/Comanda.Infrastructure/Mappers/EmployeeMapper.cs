namespace Comanda.Infrastructure.Mappers;

using Comanda.Database.Entities;
using Comanda.Domain.Entities;

public static class EmployeeMapper
{
    extension(EmployeeDatabaseEntity dbEntity)
    {
        public Employee FromPersistence() => 
            Employee.Rehydrate(
                dbEntity.PublicId,
                dbEntity.UserName ?? string.Empty, // IdentityUser properties can be null, but in our domain they are required
                dbEntity.Email ?? string.Empty, // IdentityUser properties can be null, but in our domain they are required
                dbEntity.PhoneNumber,
                dbEntity.ApiKey,
                dbEntity.ApiKeyCreatedAt,
                !dbEntity.LockoutEnabled, // If lockout from IdentityUser is enabled, employee is inactive
                dbEntity.CreatedAt);
    }

    extension(Employee domainEntity)
    {
        public EmployeeDatabaseEntity ToPersistence() => 
            new()
            {
                PublicId = domainEntity.PublicId,
                UserName = domainEntity.UserName,
                Email = domainEntity.Email,
                PhoneNumber = domainEntity.PhoneNumber,
                ApiKey = domainEntity.ApiKey,
                ApiKeyCreatedAt = domainEntity.ApiKeyCreatedAt,
                CreatedAt = DateTime.UtcNow,
                LockoutEnabled = !domainEntity.IsActive,
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                AccessFailedCount = 0,
                NormalizedUserName = domainEntity.UserName.ToUpperInvariant(),
                NormalizedEmail = domainEntity.Email.ToUpperInvariant(),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

        public void UpdatePersistence(EmployeeDatabaseEntity dbEntity)
        {
            dbEntity.UserName = domainEntity.UserName;
            dbEntity.Email = domainEntity.Email;
            dbEntity.PhoneNumber = domainEntity.PhoneNumber;
            dbEntity.ApiKey = domainEntity.ApiKey;
            dbEntity.ApiKeyCreatedAt = domainEntity.ApiKeyCreatedAt;
            dbEntity.LockoutEnabled = !domainEntity.IsActive;
            dbEntity.NormalizedUserName = domainEntity.UserName.ToLower();
            dbEntity.NormalizedEmail = domainEntity.Email.ToLower();
            dbEntity.LastModifiedAt = DateTime.UtcNow;
        }
    }
}






