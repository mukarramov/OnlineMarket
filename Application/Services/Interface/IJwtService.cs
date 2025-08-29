using Domain.Models;

namespace Application.Services.Interface;

public interface IJwtService
{
    public string GenerateToken(User user);
}
