using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;

namespace Application.Services.Interface;

public interface ICartItemService
{
    CartItemResponse? Add(CartItemCreate cartItemCreate);
    IEnumerable<CartItemResponse> GetAll();
    CartItemResponse? Update(int id, CartItemCreate cartItemCreate);
    CartItemResponse? Delete(int id);
    CartItemResponse? GetById(int id);
}