using Domain.Dto.CreatedRequest;

namespace Application.Services.Interface;

public interface IJwtService
{
    public string GenerateToken(AuthUser user);
}