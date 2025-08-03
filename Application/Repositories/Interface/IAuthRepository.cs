using Domain.Dto.CreatedRequest;

namespace Application.Repositories.Interface;

public interface IAuthRepository
{
    Task Create(AuthUser user);
    Task<AuthUser?> GetByUserEmail(string email);
}