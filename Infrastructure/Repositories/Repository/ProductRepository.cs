using Application.Repositories.Interface;
using Domain.Models;
using Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.Repository;

public class ProductRepository(AppDbContext context, ILogger<Product> logger) : IProductRepository
{
    public Product Add(Product product)
    {
        // for (int i = product.Quantity; i >= 0; i--)
        // {
        context.Products.Add(product);
        context.SaveChanges();
        // }

        return product;
    }

    public IEnumerable<Product> GetAll()
    {
        return context.Products.Include(x => x.Category).ToList();
    }

    public IEnumerable<Product>? GetProductByPagination(int page, int pageSize)
    {
        var users = context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        if (users.Count <= 0)
        {
            return null;
        }

        return users;
    }

    public Product? Update(Product productCreate)
    {
        var firstOrDefault = context.Products.FirstOrDefault(x => x.Id == productCreate.Id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {product}", firstOrDefault);

            return null;
        }

        context.Products.Update(firstOrDefault);
        context.SaveChanges();

        return firstOrDefault;
    }

    public Product? Delete(int id)
    {
        var firstOrDefault = context.Products.FirstOrDefault(x => x.Id == id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {product}", firstOrDefault);

            return null;
        }

        context.Remove(firstOrDefault);
        context.SaveChanges();

        return firstOrDefault;
    }

    public Product? GetById(int id)
    {
        var firstOrDefault = context.Products.FirstOrDefault(x => x.Id == id);
        if (firstOrDefault is null)
        {
            logger.LogError("can not found the {product}", firstOrDefault);

            return null;
        }

        return firstOrDefault;
    }
}