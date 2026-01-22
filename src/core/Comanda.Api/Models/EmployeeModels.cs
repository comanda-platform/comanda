namespace Comanda.Api.Models;

public record CreateEmployeeRequest(
    string UserName,
    string Email,
    string? PhoneNumber = null);

public record UpdateEmployeeRequest(
    string UserName,
    string Email,
    string? PhoneNumber = null);

public record EmployeeResponse(
    string PublicId,
    string UserName,
    string? Email,
    string? PhoneNumber,
    bool IsActive,
    bool HasApiKey,
    DateTime CreatedAt);







