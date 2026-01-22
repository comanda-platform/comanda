namespace Comanda.Application.Services;

using Comanda.Domain.Entities;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(string userName, string email, string password);
    Task<AuthResult> LoginAsync(string email, string password);
    Task<Employee?> GetEmployeeByIdAsync(int id);
}

public record AuthResult(
    bool Success,
    Employee? Employee = null,
    IEnumerable<string>? Errors = null);







