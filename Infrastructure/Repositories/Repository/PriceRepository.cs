using Application.Repositories;
using Domain.Models;
using Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Repository;

public class PriceRepository(AppDbContext context) : IPriceRepository
{
    public double Price(double price)
    {
        if (price < 0)
        {
            throw new Exception("value can not be less then 0");
        }

        var price1 = new Price
        {
            PercentageOfPrice = price
        };

        var lastOrDefault = context.Prices.OrderBy(x => x.Id).LastOrDefault();

        if (lastOrDefault is not null)
        {
            var firstOrDefault = context.Prices.FirstOrDefault(x => x.Id == lastOrDefault.Id);

            if (firstOrDefault is not null)
            {
                firstOrDefault.PercentageOfPrice = price;
                context.Prices.Update(firstOrDefault);
                context.SaveChanges();

                return price;
            }
        }

        context.Prices.Update(price1);
        context.SaveChanges();

        return price;
    }
}
