using Application.Repositories.Interface;
using Application.Services.Interface;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services.Service;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    IJwtService jwtService,
    ILogger<User> logger) : IUserService
{
    public async Task<string?> LogIn(string email, string password)
    {
        var user = await userRepository.GetByUserEmail(email);

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

        var lookForUser = await userRepository.GetByUserEmail(user.Email);

        if (lookForUser != null)
        {
            throw new NullReferenceException($"the email: {user.Email} has already exist!");
        }

        var map = mapper.Map<User>(user);

        await userRepository.Create(map);

        return user;
    }

    public IEnumerable<UserResponse> GetAll()
    {
        return userRepository.GetAll()
            .Select(mapper.Map<UserResponse>);
    }

    public IEnumerable<UserResponse> GetUserByPagination(int page, int pageSize)
    {
        var userByPagination = userRepository.GetUserByPagination(page, pageSize);

        return (userByPagination ?? throw new InvalidOperationException()).Select(mapper.Map<UserResponse>);
    }

    public UserResponse? Update(int id, UserCreate userCreate)
    {
        var user = userRepository.GetById(id);

        if (user is null)
        {
            return null;
        }

        user.Id = id;

        var map = mapper.Map(userCreate, user);

        userRepository.Update(map);

        logger.LogInformation("update {user} successfully passed", user);

        return mapper.Map<UserResponse>(map);
    }

    public UserResponse? Delete(int id)
    {
        var user = userRepository.Delete(id);

        return user is null ? null : mapper.Map<UserResponse>(user);
    }

    public UserResponse? GetById(int id)
    {
        var user = userRepository.GetById(id);

        return user is null ? null : mapper.Map<UserResponse>(user);
    }
}
