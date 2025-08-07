using Application.Repositories.Interface;
using Domain.Models;
using Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.Repository;

public class OrderItemRepository(AppDbContext context, ILogger<OrderItem> logger) : IOrderItemRepository
{
    public OrderItem Add(OrderItem orderItem)
    {
        context.OrderItems.Add(orderItem);
        context.SaveChanges();

        return orderItem;
    }

    public IEnumerable<OrderItem> GetAll()
    {
        return context.OrderItems.Include(x => x.Order)
            .Include(x => x.Product).ToList();
    }
    
    public IEnumerable<OrderItem>? GetOrderItemByPagination(int page, int pageSize)
    {
        var users = context.OrderItems.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        if (users.Count <= 0)
        {
            return null;
        }

        return users;
    }

    public OrderItem? Update(OrderItem orderItem)
    {
        var firstOrDefault = context.OrderItems.FirstOrDefault(x => x.Id == orderItem.Id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {orderItem}", firstOrDefault);

            return null;
        }

        context.OrderItems.Update(firstOrDefault);
        context.SaveChanges();

        return firstOrDefault;
    }

    public OrderItem? Delete(int id)
    {
        var firstOrDefault = context.OrderItems.FirstOrDefault(x => x.Id == id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {orderItem}", firstOrDefault);

            return null;
        }

        context.Remove(firstOrDefault);
        context.SaveChanges();

        return firstOrDefault;
    }

    public OrderItem? GetById(int id)
    {
        var firstOrDefault = context.OrderItems.FirstOrDefault(x => x.Id == id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {orderItem}", firstOrDefault);

            return null;
        }

        return firstOrDefault;
    }
}