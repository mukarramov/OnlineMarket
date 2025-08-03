using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;

namespace Application.Services.Interface;

public interface IShoppingCartService
{
    ShoppingCartResponse? Add(ShoppingCartCreate shoppingCartCreate);
    IEnumerable<ShoppingCartResponse> GetAll();
    ShoppingCartResponse? Update(Guid id, ShoppingCartCreate shoppingCartCreate);
    ShoppingCartResponse? Delete(Guid id);
    ShoppingCartResponse? GetById(Guid id);
}