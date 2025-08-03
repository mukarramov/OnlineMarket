using Application.Services.Interface;
using Domain.Dto.CreatedRequest;
using Microsoft.AspNetCore.Mvc;

namespace IT_RunCourseSecondPartAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ShoppingCartController(IShoppingCartService shoppingCartService) : ControllerBase
{
    [HttpPost]
    public IActionResult Add(ShoppingCartCreate shoppingCartCreate)
    {
        return Ok(shoppingCartService.Add(shoppingCartCreate));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(shoppingCartService.GetAll());
    }

    [HttpPut]
    public IActionResult Update(Guid id, ShoppingCartCreate shoppingCartCreate)
    {
        var shoppingCartResponse = shoppingCartService.Update(id, shoppingCartCreate);

        if (shoppingCartResponse is null)
        {
            return NotFound();
        }

        return Ok(shoppingCartResponse);
    }

    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        var shoppingCartResponse = shoppingCartService.Delete(id);

        if (shoppingCartResponse is null)
        {
            return NotFound();
        }

        return Ok(shoppingCartResponse);
    }

    [HttpGet]
    public IActionResult GetById(Guid id)
    {
        var shoppingCartResponse = shoppingCartService.GetById(id);

        if (shoppingCartResponse is null)
        {
            return NotFound();
        }

        return Ok(shoppingCartResponse);
    }
}