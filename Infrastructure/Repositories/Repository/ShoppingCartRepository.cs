using Application.Repositories.Interface;
using Domain.Models;
using Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.Repository;

public class ShoppingCartRepository(AppDbContext context, ILogger<ShoppingCart> logger) : IShoppingCartRepository
{
    public ShoppingCart Add(ShoppingCart shoppingCart)
    {
        context.ShoppingCarts.Add(shoppingCart);
        context.SaveChanges();

        return shoppingCart;
    }

    public IEnumerable<ShoppingCart> GetAll()
    {
        var shoppingCarts = context.ShoppingCarts
            .Include(x => x.User).ToList();

        return shoppingCarts;
    }

    public ShoppingCart? Update(ShoppingCart shoppingCart)
    {
        var firstOrDefault = context.ShoppingCarts.FirstOrDefault(x => x.Id == shoppingCart.Id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {shoppingCart}", firstOrDefault);

            return null;
        }

        context.ShoppingCarts.Update(shoppingCart);
        context.SaveChanges();

        return shoppingCart;
    }

    public ShoppingCart? Delete(int id)
    {
        var firstOrDefault = context.ShoppingCarts.FirstOrDefault(x => x.Id == id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {shoppingCart}", firstOrDefault);

            return null;
        }

        context.ShoppingCarts.Remove(firstOrDefault);
        context.SaveChanges();

        return firstOrDefault;
    }

    public ShoppingCart? GetById(int id)
    {
        var firstOrDefault = context.ShoppingCarts.FirstOrDefault(x => x.Id == id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {shoppingCart}", firstOrDefault);

            return null;
        }

        return firstOrDefault;
    }

    public ShoppingCart? GetByUserId(int id)
    {
        var firstOrDefault = context.ShoppingCarts
            .Include(x => x.CartItems)
            .FirstOrDefault(x => x.UserId == id);
        if (firstOrDefault is null)
        {
            return null;
        }

        return firstOrDefault;
    }
}