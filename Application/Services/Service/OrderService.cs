using Application.Repositories.Interface;
using Application.Services.Interface;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services.Service;

public class OrderService(
    IOrderRepository orderRepository,
    IUserRepository userRepository,
    IOrderItemRepository orderItemRepository,
    IShoppingCartRepository shoppingCartRepository,
    ICartItemRepository cartItemRepository,
    IMapper mapper,
    ILogger<Order> logger) : IOrderService
{
    public OrderResponse? Add(OrderCreate orderCreate)
    {
        var userById = userRepository.GetById(orderCreate.UserId);
        if (userById is null)
        {
            return null;
        }

        var order = mapper.Map<Order>(orderCreate);

        var shoppingCart = shoppingCartRepository.GetByUserId(orderCreate.UserId);
        if (shoppingCart?.CartItems is null)
        {
            return null;
        }

        order.UserId = shoppingCart.UserId;
        order.User = shoppingCart.User;
        var selectOrderItems = shoppingCart.CartItems.Select(x => new OrderItem
        {
            ProductId = x.ProductId,
            Product = x.Product,
            Quantity = x.Quantity,
            TotalPrice = x.TotalPrice,
            OrderId = order.Id,
            Order = order,
            CreateAt = x.CreateAt,
            UpdateAt = x.UpdateAt,
            IsDeleted = x.IsDeleted
        }).ToList();

        order.OrderItems = selectOrderItems;
        order.TotalPrice = shoppingCart.TotalPrice;
        order.CreateAt = shoppingCart.CreateAt;
        order.UpdateAt = shoppingCart.UpdateAt;
        order.IsDeleted = shoppingCart.IsDeleted;

        orderRepository.Add(order);

        selectOrderItems.Select(orderItemRepository.Add);

        var cartItems = cartItemRepository.GetAll()
            .Where(x => x.ShoppingCartId == shoppingCart.Id);

        cartItems.Select(x => cartItemRepository.Delete(x.Id));

        shoppingCartRepository.Delete(shoppingCart.Id);

        return mapper.Map<OrderResponse>(order);
    }

    public IEnumerable<OrderResponse> GetAll()
    {
        return orderRepository.GetAll()
            .Select(mapper.Map<OrderResponse>);
    }
    
    public IEnumerable<OrderResponse> GetOrderByPagination(int page, int pageSize)
    {
        var orderByPagination = orderRepository.GetOrderByPagination(page, pageSize);

        return orderByPagination.Select(mapper.Map<OrderResponse>);
    }

    public OrderResponse? Update(int id, OrderCreate orderCreate)
    {
        var order = orderRepository.GetById(id);
        var userById = userRepository.GetById(orderCreate.UserId);

        if (order is null || userById is null)
        {
            return null;
        }

        order.Id = id;
        order.UserId = userById.Id;
        order.User = userById;

        orderRepository.Update(order);

        return mapper.Map<OrderResponse>(order);
    }

    public OrderResponse? Delete(int id)
    {
        var order = orderRepository.Delete(id);

        return order is null ? null : mapper.Map<OrderResponse>(order);
    }

    public OrderResponse? GetById(int id)
    {
        var order = orderRepository.GetById(id);

        return order is null ? null : mapper.Map<OrderResponse>(order);
    }
}