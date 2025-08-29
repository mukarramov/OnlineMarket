using Application.Repositories;
using Application.Services.Interface;
using Domain.Models;

namespace Application.Services.Service;

public class PriceService(IPriceRepository priceRepository) : IPriceService
{
    public double Price(double price)
    {
        return priceRepository.Price(price);
    }
}
