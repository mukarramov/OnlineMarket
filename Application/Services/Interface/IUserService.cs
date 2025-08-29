using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;

namespace Application.Services.Interface;

public interface IUserService
{
    Task<string?> LogIn(string email, string password);
    Task<AuthUser> Registration(AuthUser user);

    IEnumerable<UserResponse> GetAll();
    IEnumerable<UserResponse> GetUserByPagination(int page, int pageSize);
    UserResponse? Update(int id, UserCreate userCreate);
    UserResponse? Delete(int id);

    UserResponse? GetById(int id);
}
