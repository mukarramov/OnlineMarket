using Application.Repositories.Interface;
using Application.Services.Interface;
using Application.Services.Service;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;

namespace OnlineShop.Test;

public class UserServiceTest
{
    private readonly Mock<IUserRepository> _mockIUserRepository = new();
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly Mock<IJwtService> _mockIJwtService = new();
    private readonly Mock<ILogger<User>> _mockLogger = new();

    private readonly UserService _userService;

    public UserServiceTest()
    {
        this._userService = new UserService(
            this._mockIUserRepository.Object,
            this._mockMapper.Object,
            this._mockIJwtService.Object,
            this._mockLogger.Object);
    }

    [Fact]
    public async Task Registration_WhenEmailNotNullAndNotExist_ThenMapToUser()
    {
        // Arrange
        var authUser = new AuthUser
        {
            Email = "alidsddd@gmail.com",
            Password = "pass2",
        };

        var user = new User
        {
            Email = authUser.Email,
            Password = authUser.Password,
        };

        var response = new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.Password,
        };

        this._mockMapper.Setup(x => x.Map<User>(
                It.IsAny<UserCreate>()))
            .Returns(user);

        this._mockIJwtService.Setup(x => x.GenerateToken(
                It.IsAny<User>()))
            .Returns(It.IsAny<string>());

        this._mockIUserRepository.Setup(x => x.Add(
                It.IsAny<User>()))
            .Returns(user);

        this._mockMapper.Setup(x => x.Map<UserResponse>(
                It.IsAny<User>()))
            .Returns(response);

        // Act
        var userResponse = await this._userService.Registration(authUser);

        // Assert
        Assert.Equal(authUser.Email, userResponse.Email);
        Assert.Equal(authUser.Password, userResponse.Password);
    }

    [Fact]
    public void UpdateUser_WhenUserIdNotNull_ThenMapToUser()
    {
        // Arrange
        var userCreate = new UserCreate
        {
            FullName = "Ali",
            Email = "alidsddd@gmail.com",
            Password = "pass2",
            Address = "32"
        };

        var user = new User
        {
            Id = 1,
            FullName = userCreate.FullName,
            Email = userCreate.Email,
            Password = userCreate.Password,
            Address = userCreate.Address
        };

        var response = new UserResponse
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Password = user.Password,
            Address = user.Address
        };

        this._mockMapper.Setup(x => x.Map<User>(
                It.IsAny<UserCreate>()))
            .Returns(user);

        this._mockIUserRepository.Setup(x => x.GetById(
                It.IsAny<int>()))
            .Returns(user);

        this._mockIUserRepository.Setup(x => x.Update(
                It.IsAny<User>()))
            .Returns(user);

        this._mockMapper.Setup(x => x.Map<UserResponse>(
                It.IsAny<User>()))
            .Returns(response);

        // Act
        var userResponse = this._userService.Update(1, userCreate);

        // Assert
        Assert.NotNull(userResponse);

        Assert.Equal(1, user.Id);
        Assert.Equal(userCreate.FullName, userResponse.FullName);
        Assert.Equal(userCreate.Email, userResponse.Email);
        Assert.Equal(userCreate.Password, userResponse.Password);
        Assert.Equal(userCreate.Address, userResponse.Address);
    }
}
