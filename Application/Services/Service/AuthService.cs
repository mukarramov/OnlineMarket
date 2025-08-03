using Application.Repositories.Interface;
using Application.Services.Interface;
using Domain.Dto.CreatedRequest;

namespace Application.Services.Service;

public class AuthService(
    IAuthRepository authRepository,
    IJwtService jwtService) : IAuthService
{
    public async Task<string?> LogIn(string email, string password)
    {
        var user = await authRepository.GetByUserEmail(email);

        if (user == null)
        {
            throw new NullReferenceException($"email: {email} not found!");
        }

        var hashPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!hashPassword)
        {
            throw new KeyNotFoundException($"password is incorrect!");
        }

        return jwtService.GenerateToken(user);
    }

    public async Task<AuthUser> Registration(AuthUser user)
    {
        if (user.Email is null)
        {
            throw new NullReferenceException();
        }

        var lookForUser = await authRepository.GetByUserEmail(user.Email);

        if (lookForUser != null)
        {
            throw new NullReferenceException($"the email: {user.Email} has already exist!");
        }

        await authRepository.Create(user);

        return user;
    }
}