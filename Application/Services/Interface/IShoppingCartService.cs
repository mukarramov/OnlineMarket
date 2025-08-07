using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;

namespace Application.Services.Interface;

public interface IShoppingCartService
{
    ShoppingCartResponse? Add(ShoppingCartCreate shoppingCartCreate);
    IEnumerable<ShoppingCartResponse> GetAll();
    ShoppingCartResponse? Update(int id, ShoppingCartCreate shoppingCartCreate);
    ShoppingCartResponse? Delete(int id);
    ShoppingCartResponse? GetById(int id);
}