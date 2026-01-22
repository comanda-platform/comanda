namespace Comanda.Api.Models;

/// <summary>
/// Request model for patching an employee
/// </summary>
public record PatchEmployeeRequest
{
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public bool? IsActive { get; init; }
    public bool? GenerateApiKey { get; init; }
    public bool? RevokeApiKey { get; init; }
}

/// <summary>
/// Query parameters for filtering employees
/// </summary>
public record EmployeeQueryParameters
{
    public bool? Active { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
}







