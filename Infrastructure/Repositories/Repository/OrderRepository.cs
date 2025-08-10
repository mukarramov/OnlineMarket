using Application.Repositories.Interface;
using Domain.Models;
using Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.Repository;

public class OrderRepository(AppDbContext context, ILogger<Order> logger) : IOrderRepository
{
    public Order Add(Order orderItem)
    {
        context.Orders.Add(orderItem);
        context.SaveChanges();

        return orderItem;
    }

    public IEnumerable<Order> GetAll()
    {
        var orders = context.Orders
            .Include(x => x.User).ToList();

        return orders;
    }

    public IEnumerable<Order>? GetOrderByPagination(int page, int pageSize, int userId)
    {
        var users = context.Orders.Skip((page - 1) * pageSize).Take(pageSize)
            .Where(x => x.UserId == userId).ToList();

        if (users.Count <= 0)
        {
            return null;
        }

        return users;
    }

    public Order? Update(Order user, int userId)
    {
        var firstOrDefault = context.Orders.FirstOrDefault(x => x.Id == user.Id && x.UserId == userId);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {order}", firstOrDefault);

            return null;
        }

        context.Orders.Update(firstOrDefault);
        context.SaveChanges();

        return firstOrDefault;
    }

    public Order? Delete(int id, int userId)
    {
        var firstOrDefault = context.Orders.FirstOrDefault(x => x.Id == id && x.UserId == userId);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {order}", firstOrDefault);

            return null;
        }

        context.Remove(firstOrDefault);
        context.SaveChanges();

        return firstOrDefault;
    }

    public Order? GetById(int id)
    {
        var firstOrDefault = context.Orders.FirstOrDefault(x => x.Id == id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {order}", firstOrDefault);

            return null;
        }

        return firstOrDefault;
    }

    public IEnumerable<Order> GetOrdersByUserId(int userId, int userRole)
    {
        var orders = context.Orders.Where(x => x.UserId == userId).ToList();

        return orders;
    }
}