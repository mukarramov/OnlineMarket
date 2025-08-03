using Application.Repositories.Interface;
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
    private readonly Mock<IValidator<UserCreate>> _mockValidator = new();
    private readonly Mock<ILogger<User>> _mockLogger = new();

    private readonly UserService _userService;

    public UserServiceTest()
    {
        _userService = new UserService(
            _mockIUserRepository.Object,
            _mockMapper.Object,
            _mockValidator.Object,
            _mockLogger.Object);
    }

    [Fact]
    public void AddUser_WhenPassValidator_ThenMapToUser()
    {
        // Arrange
        var userCreate = new UserCreate
        {
            FullName = "Ali", Email = "alidsddd@gmail.com", Password = "pass2", Address = "32"
        };

        var user = new User
        {
            FullName = userCreate.FullName, Email = userCreate.Email, Password = userCreate.Password,
            Address = userCreate.Address
        };

        var response = new UserResponse
        {
            FullName = user.FullName, Email = user.Email, Password = user.Password, Address = user.Address
        };

        _mockMapper.Setup(x => x.Map<User>(
                It.IsAny<UserCreate>()))
            .Returns(user);

        _mockValidator.Setup(x => x.Validate(
                It.IsAny<UserCreate>()))
            .Returns(new ValidationResult());

        _mockIUserRepository.Setup(x => x.Add(
                It.IsAny<User>()))
            .Returns(user);

        _mockMapper.Setup(x => x.Map<UserResponse>(
                It.IsAny<User>()))
            .Returns(response);

        // Act
        var userResponse = _userService.Add(userCreate);

        // Assert
        Assert.Equal(userCreate.FullName, userResponse.FullName);
        Assert.Equal(userCreate.Email, userResponse.Email);
        Assert.Equal(userCreate.Password, userResponse.Password);
        Assert.Equal(userCreate.Address, userResponse.Address);
    }
}