using Domain.Models;

namespace Application.Repositories.Interface;

public interface IOrderRepository
{
    Order Add(Order order);
    IEnumerable<Order> GetAll();
    IEnumerable<Order>? GetOrderByPagination(int page, int pageSize);
    Order? Update(Order order);
    Order? Delete(int id);

    Order? GetById(int id);
}