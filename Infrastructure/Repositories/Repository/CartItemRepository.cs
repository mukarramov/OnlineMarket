using Application.Repositories.Interface;
using Domain.Models;
using Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.Repository;

public class CartItemRepository(AppDbContext context, ILogger<CartItem> logger) : ICartItemRepository
{
    public CartItem Add(CartItem cartItem)
    {
        context.CartItems.Add(cartItem);
        context.SaveChanges();

        return cartItem;
    }

    public IEnumerable<CartItem> GetAll()
    {
        return context.CartItems.Include(x => x.Product).ThenInclude(x => x!.Category)
            .Include(x => x.ShoppingCart).ToList();
    }

    public CartItem? Update(CartItem cartItem)
    {
        context.CartItems.Update(cartItem);
        context.SaveChanges();

        return cartItem;
    }

    public CartItem? Delete(int id)
    {
        var firstOrDefault = context.CartItems.FirstOrDefault(x => x.Id == id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {cartItem}", firstOrDefault);

            return null;
        }

        context.CartItems.Remove(firstOrDefault);
        context.SaveChanges();

        return firstOrDefault;
    }

    public CartItem? GetById(int id)
    {
        var firstOrDefault = context.CartItems.FirstOrDefault(x => x.Id == id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {cartItem}", firstOrDefault);

            return null;
        }

        return firstOrDefault;
    }

    public CartItem? GetByProductAndShoppingCartId(int productId, int shoppingCartId)
    {
        var cartItem = context.CartItems.FirstOrDefault(x => x.ProductId == productId
                                                             && x.ShoppingCartId == shoppingCartId);
        if (cartItem is null)
        {
            return null;
        }

        return cartItem;
    }
}