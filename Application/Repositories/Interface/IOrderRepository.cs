using Domain.Models;

namespace Application.Repositories.Interface;

public interface IOrderRepository
{
    Order Add(Order order);
    IEnumerable<Order> GetAll();
    IEnumerable<Order>? GetOrderByPagination(int page, int pageSize, int userId);
    Order? Update(Order order, int userId);
    Order? Delete(int id, int userId);

    Order? GetById(int id);
    IEnumerable<Order> GetOrdersByUserId(int userId, int userRole);
}