using Domain.Models;

namespace Application.Repositories.Interface;

public interface IOrderItemRepository
{
    OrderItem Add(OrderItem orderItem);
    IEnumerable<OrderItem> GetAll();
    IEnumerable<OrderItem>? GetOrderItemByPagination(int page, int pageSize);
    OrderItem? Update(OrderItem orderItem);
    OrderItem? Delete(int id);

    OrderItem? GetById(int id);
}