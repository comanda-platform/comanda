namespace Comanda.Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Comanda.Application.Services;
using Comanda.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService(IConfiguration config) : ITokenService
{
    private readonly IConfiguration _config = config;

    public string GenerateJwtToken(Employee employee)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, employee.PublicId),
            new Claim(JwtRegisteredClaimNames.Email, employee.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, employee.PublicId),
            new Claim(ClaimTypes.Name, employee.UserName),
            new Claim("PublicId", employee.PublicId)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateApiKey()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }
}







