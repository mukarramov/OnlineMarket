using Domain.Dto.CreatedRequest;

namespace Application.Services.Interface;

public interface IAuthService
{
    Task<string?> LogIn(string email, string password);
    Task<AuthUser> Registration(AuthUser user);
}