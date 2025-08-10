using Application.Repositories.Interface;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Models;
using Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.Repository;

public class UserRepository(AppDbContext context, ILogger<User> logger) : IUserRepository
{
    
    public async Task Create(User user)
    {
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        user.Password = hashPassword;
        user.CreateAt = DateTime.UtcNow;

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetByUserEmail(string email)
    {
        var userByEmail = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        return userByEmail;
    }
    public User Add(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();

        return user;
    }

    public IEnumerable<User> GetAll()
    {
        return context.Users.ToList();
    }

    public IEnumerable<User>? GetUserByPagination(int page, int pageSize)
    {
        var users = context.Users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        if (users.Count <= 0)
        {
            return null;
        }

        return users;
    }

    public User? Update(User user)
    {
        var firstOrDefault = context.Users.FirstOrDefault(x => x.Id == user.Id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {user}", firstOrDefault);

            return null;
        }

        context.Users.Update(firstOrDefault);
        context.SaveChanges();

        return firstOrDefault;
    }

    public User? Delete(int id)
    {
        var firstOrDefault = context.Users.FirstOrDefault(x => x.Id == id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {user}", firstOrDefault);

            return null;
        }

        context.Users.Remove(firstOrDefault);
        context.SaveChanges();

        return firstOrDefault;
    }

    public User? GetById(int id)
    {
        var firstOrDefault = context.Users.FirstOrDefault(x => x.Id == id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {user}", firstOrDefault);

            return null;
        }

        return firstOrDefault;
    }
}