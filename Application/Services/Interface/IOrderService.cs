using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;

namespace Application.Services.Interface;

public interface IOrderService
{
    OrderResponse? Add(OrderCreate orderCreate);
    IEnumerable<OrderResponse> GetAll();
    IEnumerable<OrderResponse> GetOrderByPagination(int page, int pageSize, int userId);
    OrderResponse? Update(int id, OrderCreate orderCreate, int userId);
    OrderResponse? Delete(int id, int userId);

    OrderResponse? GetById(int id);
    IEnumerable<OrderResponse> GetOrdersByUserId(int userId, int userRole);
}