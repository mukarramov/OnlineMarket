using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;

namespace Application.Services.Interface;

public interface IUserService
{
    UserResponse Add(UserCreate userCreate);
    IEnumerable<UserResponse> GetAll();
    IEnumerable<UserResponse> GetUserByPagination(int page, int pageSize);
    UserResponse? Update(int id, UserCreate userCreate);
    UserResponse? Delete(int id);

    UserResponse? GetById(int id);
}