using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;

namespace Application.Services.Interface;

public interface IOrderService
{
    OrderResponse? Add(OrderCreate orderCreate);
    IEnumerable<OrderResponse> GetAll();
    IEnumerable<OrderResponse> GetOrderByPagination(int page, int pageSize);
    OrderResponse? Update(Guid id, OrderCreate orderCreate);
    OrderResponse? Delete(Guid id);

    OrderResponse? GetById(Guid id);
}