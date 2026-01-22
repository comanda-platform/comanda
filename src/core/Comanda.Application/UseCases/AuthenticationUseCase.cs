namespace Comanda.Application.UseCases;

using Comanda.Application.Services;
using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class AuthenticationUseCase(
    IAuthService authService,
    ITokenService tokenService,
    IEmployeeRepository employeeRepository)
{
    private readonly IAuthService _authService = authService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<AuthenticationResult> RegisterAsync(string userName, string email, string password)
    {
        var result = await _authService.RegisterAsync(userName, email, password);

        if (!result.Success || result.Employee == null)
        {
            return new AuthenticationResult(
                Success: false,
                Errors: result.Errors);
        }

        var token = _tokenService.GenerateJwtToken(result.Employee);

        return new AuthenticationResult(
            Success: true,
            Token: token,
            Employee: result.Employee);
    }

    public async Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        var result = await _authService.LoginAsync(email, password);

        if (!result.Success || result.Employee == null)
        {
            return new AuthenticationResult(
                Success: false,
                Errors: result.Errors);
        }

        var token = _tokenService.GenerateJwtToken(result.Employee);

        return new AuthenticationResult(
            Success: true,
            Token: token,
            Employee: result.Employee);
    }

    public async Task<Employee?> GetCurrentEmployeeAsync(int employeeId)
    {
        return await _employeeRepository.GetByIdAsync(employeeId);
    }

    public async Task<string> GenerateApiKeyAsync(int employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId)
            ?? throw new NotFoundException("Employee not found");

        employee.GenerateApiKey();
        await _employeeRepository.UpdateAsync(employee);

        return employee.ApiKey!;
    }
}

public record AuthenticationResult(
    bool Success,
    string? Token = null,
    Employee? Employee = null,
    IEnumerable<string>? Errors = null);







