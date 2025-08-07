using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;

namespace Application.Services.Interface;

public interface IOrderItemService
{
    OrderItemResponse? Add(OrderItemCreate orderItemRequest);
    IEnumerable<OrderItemResponse> GetAll();
    IEnumerable<OrderItemResponse> GetOrderItemByPagination(int page, int pageSize);
    OrderItemResponse? Update(int id, OrderItemCreate orderItemRequest);
    OrderItemResponse? Delete(int id);

    OrderItemResponse? GetById(int id);
}