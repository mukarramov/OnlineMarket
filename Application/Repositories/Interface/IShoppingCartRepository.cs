using Domain.Models;

namespace Application.Repositories.Interface;

public interface IShoppingCartRepository
{
    ShoppingCart Add(ShoppingCart shoppingCart);
    IEnumerable<ShoppingCart> GetAll();
    ShoppingCart? Update(ShoppingCart shoppingCart);
    ShoppingCart? Delete(int id);

    ShoppingCart? GetById(int id);
    ShoppingCart? GetByUserId(int id);
}