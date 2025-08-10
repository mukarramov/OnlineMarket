using Application.Repositories.Interface;
using Application.Services.Interface;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services.Service;

public class ProductService(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IMapper mapper,
    ILogger<Product> logger)
    : IProductService
{
    public ProductResponse? Add(ProductCreate productCreate)
    {
        if (string.IsNullOrEmpty(productCreate.Name))
        {
            throw new Exception();
        }

        var categoryById = categoryRepository.GetById(productCreate.CategoryId);
        if (categoryById is null)
        {
            return null;
        }

        var product = mapper.Map<Product>(productCreate);

        product.CategoryId = categoryById.Id;
        product.Category = categoryById;

        productRepository.Add(product);

        return mapper.Map<ProductResponse>(product);
    }

    public IEnumerable<ProductResponse> GetAll()
    {
        return productRepository.GetAll()
            .Select(mapper.Map<ProductResponse>);
    }
    
    public IEnumerable<ProductResponse> GetProductByPagination(int page, int pageSize)
    {
        var productByPagination = productRepository.GetProductByPagination(page, pageSize);

        return (productByPagination ?? throw new InvalidOperationException()).Select(mapper.Map<ProductResponse>);
    }

    public ProductResponse? Update(int id, ProductCreate productCreate)
    {
        var product = productRepository.GetById(id);
        if (product is null)
        {
            return null;
        }

        var categoryById = categoryRepository.GetById(productCreate.CategoryId);
        if (categoryById is null)
        {
            return null;
        }

        var map = mapper.Map(productCreate, product);

        map.Id = id;
        map.CategoryId = categoryById.Id;
        map.Category = categoryById;

        productRepository.Update(map);

        logger.LogInformation("update {product} successfully passed", product);

        return mapper.Map<ProductResponse>(map);
    }

    public ProductResponse? Delete(int id)
    {
        var product = productRepository.Delete(id);

        return product is null ? null : mapper.Map<ProductResponse>(product);
    }

    public ProductResponse? GetById(int id)
    {
        var product = productRepository.GetById(id);

        return product is null ? null : mapper.Map<ProductResponse>(product);
    }
}