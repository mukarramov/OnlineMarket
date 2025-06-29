using Domain.Models;

namespace Application.Repositories.Interface;

public interface IOrderRepository
{
    Order Add(Order order);
    IEnumerable<Order> GetAll();
    IEnumerable<Order> GetOrderByPagination(int page, int pageSize);
    Order? Update(Order order);
    Order? Delete(Guid id);

    Order? GetById(Guid id);
}