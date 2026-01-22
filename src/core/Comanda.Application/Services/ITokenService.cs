namespace Comanda.Application.Services;

using Comanda.Domain.Entities;

public interface ITokenService
{
    string GenerateJwtToken(Employee employee);
    string GenerateApiKey();
}







