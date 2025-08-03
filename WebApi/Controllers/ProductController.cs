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
        return Ok(productService.Add(product));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(productService.GetAll());
    }
    
    [HttpGet]
    public IActionResult GetProductByPagination(int page, int pageSize)
    {
        return Ok(productService.GetProductByPagination(page, pageSize));
    }

    [HttpPut]
    public IActionResult Update(Guid id, ProductCreate product)
    {
        var productResponse = productService.Update(id, product);

        if (productResponse is null)
        {
            return NotFound();
        }

        return Ok(productResponse);
    }

    [HttpDelete]
    public IActionResult Delete(Guid productId)
    {
        var productResponse = productService.Delete(productId);

        if (productResponse is null)
        {
            return NotFound();
        }

        return Ok(productResponse);
    }

    [HttpGet]
    public IActionResult GetById(Guid id)
    {
        var productResponse = productService.GetById(id);

        if (productResponse is null)
        {
            return NotFound();
        }

        return Ok(productResponse);
    }
}