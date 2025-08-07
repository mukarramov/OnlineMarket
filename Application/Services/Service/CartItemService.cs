using Application.Repositories.Interface;
using Application.Services.Interface;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services.Service;

public class CartItemService(
    ICartItemRepository cartItemRepository,
    IProductRepository productRepository,
    IShoppingCartRepository shoppingCartRepository,
    IMapper mapper,
    ILogger<CartItem> logger)
    : ICartItemService
{
    public CartItemResponse? Add(CartItemCreate cartItemCreate)
    {
        var productById = productRepository.GetById(cartItemCreate.ProductId);
        var shoppingCartById = shoppingCartRepository.GetById(cartItemCreate.ShoppingCartId);
        if (productById is null || shoppingCartById is null)
        {
            return null;
        }

        var cartItem = mapper.Map<CartItem>(cartItemCreate);

        var cartItemByProductIdAndShoppingCartId = cartItemRepository.GetByProductAndShoppingCartId(cartItemCreate.ProductId,
            cartItemCreate.ShoppingCartId);

        if (cartItemByProductIdAndShoppingCartId is not null)
        {
            mapper.Map(cartItemByProductIdAndShoppingCartId, cartItem);
            cartItem.Product = cartItemByProductIdAndShoppingCartId.Product;

            if (cartItem.Product != null)
            {
                cartItem.Quantity++;
            }
        }

        cartItem.ProductId = productById.Id;
        cartItem.Product = productById;
        cartItem.ShoppingCartId = shoppingCartById.Id;
        cartItem.ShoppingCart = shoppingCartById;

        if (cartItem.Product != null)
        {
            cartItem.TotalPrice += cartItem.Product.Price;
        }

        shoppingCartById.TotalPrice += cartItem.TotalPrice;

        if (cartItemCreate.ProductId != cartItemByProductIdAndShoppingCartId?.ProductId)
        {
            cartItemRepository.Add(cartItem);
        }

        if (cartItemCreate.ProductId == cartItemByProductIdAndShoppingCartId?.ProductId)
        {
            cartItemRepository.Update(cartItem);
        }
        
        return mapper.Map<CartItemResponse>(cartItem);
    }

    public IEnumerable<CartItemResponse> GetAll()
    {
        var cartItems = cartItemRepository.GetAll();

        return cartItems.Select(mapper.Map<CartItemResponse>);
    }

    public CartItemResponse? Update(int id, CartItemCreate cartItemCreate)
    {
        var cartItem = cartItemRepository.GetById(id);
        var productById = productRepository.GetById(cartItemCreate.ProductId);
        var shoppingCartById = shoppingCartRepository.GetById(cartItemCreate.ShoppingCartId);

        if (cartItem is null || productById is null || shoppingCartById is null)
        {
            return null;
        }

        cartItem.Id = id;
        cartItem.Product = productById;
        cartItem.ShoppingCart = shoppingCartById;

        cartItemRepository.Update(cartItem);

        logger.LogInformation("update {cartItem} successfully passed", cartItem);

        return mapper.Map<CartItemResponse>(mapper.Map(cartItemCreate, cartItem));
    }

    public CartItemResponse? Delete(int id)
    {
        var cartItem = cartItemRepository.Delete(id);

        return cartItem is null ? null : mapper.Map<CartItemResponse>(cartItem);
    }

    public CartItemResponse? GetById(int id)
    {
        var cartItem = cartItemRepository.GetById(id);

        return cartItem is null ? null : mapper.Map<CartItemResponse>(cartItem);
    }
}