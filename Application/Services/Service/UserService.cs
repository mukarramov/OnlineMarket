using Application.Repositories.Interface;
using Application.Services.Interface;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services.Service;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    IValidator<UserCreate> validator,
    ILogger<User> logger) : IUserService
{
    public UserResponse Add(UserCreate userCreate)
    {
        if (string.IsNullOrEmpty(userCreate.Email))
        {
            throw new Exception();
        }

        var user = mapper.Map<User>(userCreate);

        var result = validator.Validate(userCreate);
        if (!result.IsValid)
        {
            var errorValidate = result.Errors.Select(x => new
            {
                x.PropertyName,
                x.ErrorCode,
                x.ErrorMessage
            });

            logger.LogError("the {email} or {password} can not passed the validation", userCreate.Email,
                userCreate.Password);

            throw new Exception($"{errorValidate}");
        }

        userRepository.Add(user);

        logger.LogInformation("the {user} successfully added to db", user);

        return mapper.Map<UserResponse>(user);
    }

    public IEnumerable<UserResponse> GetAll()
    {
        return userRepository.GetAll()
            .Select(mapper.Map<UserResponse>);
    }

    public IEnumerable<UserResponse> GetUserByPagination(int page, int pageSize)
    {
        var userByPagination = userRepository.GetUserByPagination(page, pageSize);

        return userByPagination.Select(mapper.Map<UserResponse>);
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