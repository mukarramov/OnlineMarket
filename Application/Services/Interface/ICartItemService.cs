using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;

namespace Application.Services.Interface;

public interface ICartItemService
{
    CartItemResponse? Add(CartItemCreate cartItemCreate);
    IEnumerable<CartItemResponse> GetAll();
    CartItemResponse? Update(Guid id, CartItemCreate cartItemCreate);
    CartItemResponse? Delete(Guid id);
    CartItemResponse? GetById(Guid id);
}