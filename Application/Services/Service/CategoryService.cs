using Application.Repositories.Interface;
using Application.Services.Interface;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services.Service;

public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<Category> logger)
    : ICategoryService
{
    public CategoryResponse Add(CategoryCreate categoryCreate)
    {
        if (string.IsNullOrEmpty(categoryCreate.Name))
        {
            throw new Exception();
        }

        var category = mapper.Map<Category>(categoryCreate);

        categoryRepository.Add(category);

        return mapper.Map<CategoryResponse>(category);
    }

    public IEnumerable<CategoryResponse> GetAll()
    {
        return categoryRepository.GetAll()
            .Select(mapper.Map<CategoryResponse>);
    }
    
    public IEnumerable<CategoryResponse> GetCategoryByPagination(int page, int pageSize)
    {
        var categoryByPagination = categoryRepository.GetCategoryByPagination(page, pageSize);

        return categoryByPagination.Select(mapper.Map<CategoryResponse>);
    }

    public CategoryResponse? Update(Guid id, CategoryCreate entity)
    {
        var category = categoryRepository.GetById(id);
        if (category is null)
        {
            return null;
        }

        category.Id = id;
        category.Name = entity.Name;

        categoryRepository.Update(category);

        logger.LogInformation("update {category} successfully passed", category);

        return mapper.Map<CategoryResponse>(category);
    }

    public CategoryResponse? Delete(Guid id)
    {
        var category = categoryRepository.Delete(id);

        return category is null ? null : mapper.Map<CategoryResponse>(category);
    }

    public CategoryResponse? GetById(Guid id)
    {
        var category = categoryRepository.Delete(id);

        return category is null ? null : mapper.Map<CategoryResponse>(category);
    }
}