using Application.Services.Interface;
using Domain.Dto.CreatedRequest;
using Microsoft.AspNetCore.Mvc;

namespace IT_RunCourseSecondPartAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpPost]
    public IActionResult Add(ProductCreate product)
    {
        return this.Ok(productService.Add(product));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return this.Ok(productService.GetAll());
    }
    
    [HttpGet]
    public IActionResult GetProductByPagination(int page, int pageSize)
    {
        return this.Ok(productService.GetProductByPagination(page, pageSize));
    }

    [HttpPut]
    public IActionResult Update(int id, ProductCreate product)
    {
        var productResponse = productService.Update(id, product);

        if (productResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(productResponse);
    }

    [HttpDelete]
    public IActionResult Delete(int productId)
    {
        var productResponse = productService.Delete(productId);

        if (productResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(productResponse);
    }

    [HttpGet]
    public IActionResult GetById(int id)
    {
        var productResponse = productService.GetById(id);

        if (productResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(productResponse);
    }
    [HttpGet]
    public IActionResult Search(string textSearch)
    {
        var productResponse = productService.Search(textSearch);

        if (productResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(productResponse);
    }
}
