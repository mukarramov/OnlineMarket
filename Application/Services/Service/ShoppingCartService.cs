using Application.Repositories.Interface;
using Application.Services.Interface;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services.Service;

public class ShoppingCartService(
    IShoppingCartRepository shoppingCartRepository,
    IUserRepository userRepository,
    IMapper mapper,
    ILogger<ShoppingCart> logger) : IShoppingCartService
{
    public ShoppingCartResponse? Add(ShoppingCartCreate shoppingCartCreate)
    {
        var shoppingCart = mapper.Map<ShoppingCart>(shoppingCartCreate);

        var userById = userRepository.GetById(shoppingCart.UserId);
        if (userById is null)
        {
            return null;
        }

        shoppingCart.UserId = userById.Id;
        shoppingCart.User = userById;

        shoppingCartRepository.Add(shoppingCart);

        return mapper.Map<ShoppingCartResponse>(shoppingCart);
    }

    public IEnumerable<ShoppingCartResponse> GetAll()
    {
        return shoppingCartRepository.GetAll()
            .Select(mapper.Map<ShoppingCartResponse>);
    }

    public ShoppingCartResponse? Update(Guid id, ShoppingCartCreate shoppingCartCreate)
    {
        var shoppingCart = shoppingCartRepository.GetById(id);

        if (shoppingCart is null)
        {
            return null;
        }

        var user = userRepository.GetById(shoppingCartCreate.UserId);

        if (user is null)
        {
            return null;
        }

        shoppingCart.Id = id;
        shoppingCart.UserId = user.Id;
        shoppingCart.User = user;

        shoppingCartRepository.Update(shoppingCart);

        logger.LogInformation("update {shoppingCart} successfully passed", shoppingCart);

        return mapper.Map<ShoppingCartResponse>(shoppingCart);
    }

    public ShoppingCartResponse? Delete(Guid id)
    {
        var shoppingCart = shoppingCartRepository.Delete(id);

        return shoppingCart is null ? null : mapper.Map<ShoppingCartResponse>(shoppingCart);
    }

    public ShoppingCartResponse? GetById(Guid id)
    {
        var shoppingCart = shoppingCartRepository.GetById(id);

        return shoppingCart is null ? null : mapper.Map<ShoppingCartResponse>(shoppingCart);
    }
}