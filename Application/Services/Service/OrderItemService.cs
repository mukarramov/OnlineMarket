using Application.Repositories.Interface;
using Application.Services.Interface;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services.Service;

public class OrderItemService(
    IOrderItemRepository orderItemRepository,
    IProductRepository productRepository,
    IOrderRepository orderRepository,
    IMapper mapper,
    ILogger<OrderItem> logger)
    : IOrderItemService
{
    public OrderItemResponse? Add(OrderItemCreate orderItemRequest)
    {
        if (orderItemRequest is null)
        {
            throw new Exception();
        }

        var product = productRepository.GetById(orderItemRequest.ProductId);
        var order = orderRepository.GetById(orderItemRequest.OrderId);

        if (product is null || order is null)
        {
            return null;
        }

        var orderItem = mapper.Map<OrderItem>(orderItemRequest);

        orderItem.ProductId = product.Id;
        orderItem.Product = product;
        orderItem.OrderId = order.Id;
        orderItem.Order = order;

        orderItemRepository.Add(orderItem);

        return mapper.Map<OrderItemResponse>(orderItem);
    }

    public IEnumerable<OrderItemResponse> GetAll()
    {
        return orderItemRepository.GetAll()
            .Select(mapper.Map<OrderItemResponse>);
    }
    
    public IEnumerable<OrderItemResponse> GetOrderItemByPagination(int page, int pageSize)
    {
        var orderItemByPagination = orderItemRepository.GetOrderItemByPagination(page, pageSize);

        return orderItemByPagination.Select(mapper.Map<OrderItemResponse>);
    }

    public OrderItemResponse? Update(int id, OrderItemCreate orderItemRequest)
    {
        var orderItem = orderItemRepository.GetById(id);
        var product = productRepository.GetById(orderItemRequest.ProductId);
        var order = orderRepository.GetById(orderItemRequest.OrderId);

        if (orderItem is null || product is null || order is null)
        {
            return null;
        }

        var map = mapper.Map<OrderItem>(orderItemRequest);

        map.Id = id;
        map.ProductId = product.Id;
        map.Product = product;
        map.OrderId = order.Id;
        map.Order = order;

        orderItemRepository.Update(orderItem);

        logger.LogInformation("update {orderItem} successfully passed", orderItem);

        return mapper.Map<OrderItemResponse>(map);
    }

    public OrderItemResponse? Delete(int id)
    {
        var orderItem = orderItemRepository.Delete(id);

        return orderItem is null ? null : mapper.Map<OrderItemResponse>(orderItem);
    }

    public OrderItemResponse? GetById(int id)
    {
        var orderItem = orderItemRepository.GetById(id);

        return orderItem is null ? null : mapper.Map<OrderItemResponse>(orderItem);
    }
}