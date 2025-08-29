using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;

namespace Application.Services.Interface;

public interface IProductService
{
    ProductResponse? Add(ProductCreate productCreate);
    IEnumerable<ProductResponse> GetAll();
    IEnumerable<ProductResponse> GetProductByPagination(int page, int pageSize);
    ProductResponse? Update(int id, ProductCreate productCreate);
    ProductResponse? Delete(int id);

    ProductResponse? GetById(int id);
    IEnumerable<ProductResponse>? Search(string textSearch);
}
