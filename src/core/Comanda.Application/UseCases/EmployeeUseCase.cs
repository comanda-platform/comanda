namespace Comanda.Application.UseCases;

using Comanda.Domain;
using Comanda.Domain.Entities;
using Comanda.Domain.Repositories;

public class EmployeeUseCase(IEmployeeRepository employeeRepository) : UseCaseBase(EntityTypePrintNames.Employee)
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<Employee> CreateEmployeeAsync(
        string userName,
        string email,
        string? phoneNumber = null)
    {
        if (await _employeeRepository.ExistsByUserNameAsync(userName))
            throw new ConflictException($"Username '{userName}' is already taken");

        if (await _employeeRepository.ExistsByEmailAsync(email))
            throw new ConflictException($"Email '{email}' is already registered");

        var employee = new Employee(userName, email, phoneNumber);

        await _employeeRepository.AddAsync(employee);

        return employee;
    }

    public async Task<Employee> GetEmployeeByPublicIdAsync(string publicId)
        => await _employeeRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

    public async Task<Employee> GetEmployeeByUserNameAsync(string userName)
        => await _employeeRepository.GetByUserNameAsync(userName)
            ?? throw new NotFoundException(EntityTypePrintName, userName);

    public async Task<Employee> GetEmployeeByEmailAsync(string email)
        => await _employeeRepository.GetByEmailAsync(email)
            ?? throw new NotFoundException(EntityTypePrintName, email);

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        => await _employeeRepository.GetAllAsync();

    public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
        => await _employeeRepository.GetActiveEmployeesAsync();

    public async Task UpdateEmployeeAsync(
        string publicId,
        string userName,
        string email,
        string? phoneNumber)
    {
        var employee = await _employeeRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        if (employee.UserName != userName &&
            await _employeeRepository.ExistsByUserNameAsync(userName))
            throw new ConflictException($"Username '{userName}' is already taken");

        if (employee.Email != email &&
            await _employeeRepository.ExistsByEmailAsync(email))
            throw new ConflictException($"Email '{email}' is already registered");

        employee.UpdateDetails(userName, email, phoneNumber);
        await _employeeRepository.UpdateAsync(employee);
    }

    public async Task<string> GenerateApiKeyAsync(string publicId)
    {
        var employee = await _employeeRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        employee.GenerateApiKey();
        await _employeeRepository.UpdateAsync(employee);

        return employee.ApiKey!;
    }

    public async Task RevokeApiKeyAsync(string publicId)
    {
        var employee = await _employeeRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        employee.RevokeApiKey();
        await _employeeRepository.UpdateAsync(employee);
    }

    public async Task<bool> ValidateApiKeyAsync(string apiKey)
    {
        var employee = await _employeeRepository.GetByApiKeyAsync(apiKey);

        if (employee == null)
            return false;

        return employee.ValidateApiKey(apiKey);
    }

    public async Task<Employee?> GetEmployeeByApiKeyAsync(string apiKey)
    {
        var employee = await _employeeRepository.GetByApiKeyAsync(apiKey);

        if (employee == null || !employee.ValidateApiKey(apiKey))
            return null;

        return employee;
    }

    public async Task ActivateEmployeeAsync(string publicId)
    {
        var employee = await _employeeRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        employee.Activate();
        await _employeeRepository.UpdateAsync(employee);
    }

    public async Task DeactivateEmployeeAsync(string publicId)
    {
        var employee = await _employeeRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        employee.Deactivate();
        await _employeeRepository.UpdateAsync(employee);
    }

    public async Task DeleteEmployeeAsync(string publicId)
    {
        var employee = await _employeeRepository.GetByPublicIdAsync(publicId)
            ?? throw new NotFoundException(EntityTypePrintName, publicId);

        await _employeeRepository.DeleteAsync(employee);
    }
}






