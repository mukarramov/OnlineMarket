using Application.Repositories.Interface;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;
using FluentAssertions;
using NSubstitute;

namespace UserTest;

public class UnitMockTest
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();

    [Fact]
    public void AddUserFromUserService()
    {
        // Arrange
        var userCreate = new UserCreate
        {
            FullName = "khujand",
            Password = "passswefword",
            Email = "tettte@lsmk.com",
            Address = "123VStreet"
        };

        var user = new User
        {
            FullName = userCreate.FullName,
            Password = userCreate.Password,
            Email = userCreate.Email,
            Address = userCreate.Address
        };
        var userResponse = new UserResponse
        {
            FullName = user.FullName,
            Email = user.Email
        };

        _userRepository.Add(user).Returns(user);
        _mapper.Map<UserResponse>(userCreate).Returns(userResponse);

        // Act
        var result = _userRepository.Add(user);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(userCreate.Email);
        _userRepository.Received(1).Add(Arg.Is<User>(u => u.Email == userCreate.Email));
    }
}