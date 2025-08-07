using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;

namespace Application.Services.Interface;

public interface ICategoryService
{
    CategoryResponse Add(CategoryCreate categoryCreate);
    IEnumerable<CategoryResponse> GetAll();
    IEnumerable<CategoryResponse> GetCategoryByPagination(int page, int pageSize);
    CategoryResponse? Update(int id, CategoryCreate categoryCreate);
    CategoryResponse? Delete(int id);

    CategoryResponse? GetById(int id);
}