using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services.Interface;
using Domain.Dto.CreatedRequest;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Service;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public string GenerateToken(User user)
    {
        if (user.Email == null)
        {
            throw new Exception("null email!");
        }

        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new("name", user.Email),
            new("role", user.Role.ToString()),
            new("date", user.CreateAt.ToString(CultureInfo.CurrentCulture))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ??
                                                                  throw new InvalidOperationException("" +
                                                                      "the problem is with key")));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}