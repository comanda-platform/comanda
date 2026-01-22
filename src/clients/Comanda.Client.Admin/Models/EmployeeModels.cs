namespace Comanda.Client.Admin.Models;

public record EmployeeResponse(
    string PublicId,
    string UserName,
    string Email,
    string? PhoneNumber,
    bool HasApiKey,
    DateTime? ApiKeyCreatedAt,
    bool IsActive,
    DateTime CreatedAt);

public record CreateEmployeeRequest(
    string UserName,
    string Email,
    string? PhoneNumber);

public record UpdateEmployeeRequest(
    string UserName,
    string Email,
    string? PhoneNumber);

public record ApiKeyResponse(
    string ApiKey,
    DateTime CreatedAt);

public static class EmployeeExtensions
{
    public static bool IsApiKeyExpired(this EmployeeResponse employee)
    {
        if (!employee.HasApiKey || !employee.ApiKeyCreatedAt.HasValue)
            return false;

        return DateTime.UtcNow - employee.ApiKeyCreatedAt.Value > TimeSpan.FromDays(90);
    }

    public static int? ApiKeyDaysRemaining(this EmployeeResponse employee)
    {
        if (!employee.HasApiKey || !employee.ApiKeyCreatedAt.HasValue)
            return null;

        var expiryDate = employee.ApiKeyCreatedAt.Value.AddDays(90);
        var daysRemaining = (expiryDate - DateTime.UtcNow).Days;

        return daysRemaining > 0 ? daysRemaining : 0;
    }
}










