using Application.Repositories.Interface;
using Domain.Dto.CreatedRequest;
using Domain.Models;
using Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Repository;

public class AuthRepository(AppDbContext context) : IAuthRepository
{
    public async Task Create(AuthUser user)
    {
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        user.Password = hashPassword;
        user.CreateAt = DateTime.UtcNow;

        await context.AuthUsers.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<AuthUser?> GetByUserEmail(string email)
    {
        var userByEmail = await context.AuthUsers.FirstOrDefaultAsync(x => x.Email == email);
        return userByEmail;
    }
}