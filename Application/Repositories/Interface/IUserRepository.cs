using Domain.Models;

namespace Application.Repositories.Interface;

public interface IUserRepository
{
    Task Create(User user);
    Task<User?> GetByUserEmail(string email);

    User Add(User user);
    IEnumerable<User> GetAll();
    IEnumerable<User>? GetUserByPagination(int page, int pageSize);
    User? Update(User user);
    User? Delete(int id);

    User? GetById(int id);
}