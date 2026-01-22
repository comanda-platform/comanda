namespace Comanda.Infrastructure.Services;

using Microsoft.AspNetCore.Identity;
using Comanda.Application.Services;
using Comanda.Database.Entities;
using Comanda.Domain.Entities;
using Comanda.Domain.Helpers;
using Comanda.Infrastructure.Mappers;

public class AuthService(UserManager<EmployeeDatabaseEntity> userManager) : IAuthService
{
    private readonly UserManager<EmployeeDatabaseEntity> _userManager = userManager;

    public async Task<AuthResult> RegisterAsync(string userName, string email, string password)
    {
        var entity = new EmployeeDatabaseEntity
        {
            UserName = userName,
            Email = email,
            PublicId = PublicIdHelper.Generate(),
            CreatedAt = DateTime.UtcNow,
            EmailConfirmed = true,
            NormalizedUserName = userName.ToUpperInvariant(),
            NormalizedEmail = email.ToUpperInvariant()
        };

        var result = await _userManager.CreateAsync(entity, password);

        if (!result.Succeeded)
        {
            return new AuthResult(
                Success: false,
                Errors: result.Errors.Select(e => e.Description));
        }

        return new AuthResult(
            Success: true,
            Employee: entity.FromPersistence());
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var entity = await _userManager.FindByEmailAsync(email);

        if (entity == null)
        {
            return new AuthResult(
                Success: false,
                Errors: ["Invalid email or password"]);
        }

        if (!await _userManager.CheckPasswordAsync(entity, password))
        {
            return new AuthResult(
                Success: false,
                Errors: ["Invalid email or password"]);
        }

        return new AuthResult(
            Success: true,
            Employee: entity.FromPersistence());
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        var entity = await _userManager.FindByIdAsync(id.ToString());

        return entity?.FromPersistence();
    }
}







