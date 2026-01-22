using Comanda.Api.Models;

namespace Comanda.Api.Mappers;

public static class EmployeeResponseMapper
{
    public static EmployeeResponse ToResponse(Domain.Entities.Employee employee) 
        => new(
            employee.PublicId,
            employee.UserName,
            employee.Email,
            employee.PhoneNumber,
            employee.IsActive,
            employee.ApiKeyCreatedAt.HasValue,
            employee.CreatedAt);
}






