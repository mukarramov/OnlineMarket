using Application.Services.Interface;
using Domain.Dto.CreatedRequest;
using Microsoft.AspNetCore.Mvc;

namespace IT_RunCourseSecondPartAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ShoppingCartController(IShoppingCartService shoppingCartService) : ControllerBase
{
    private int UserId => int.Parse(this.User.Claims.First(i => i.Type == "id").Value);

    [HttpPost]
    public IActionResult Add(ShoppingCartCreate shoppingCartCreate)
    {
        return this.Ok(shoppingCartService.Add(shoppingCartCreate));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return this.Ok(shoppingCartService.GetAll());
    }

    [HttpPut]
    public IActionResult Update(int id, ShoppingCartCreate shoppingCartCreate)
    {
        var shoppingCartResponse = shoppingCartService.Update(id, shoppingCartCreate);

        if (shoppingCartResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(shoppingCartResponse);
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var shoppingCartResponse = shoppingCartService.Delete(id);

        if (shoppingCartResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(shoppingCartResponse);
    }

    [HttpGet]
    public IActionResult GetById(int id)
    {
        var shoppingCartResponse = shoppingCartService.GetById(id);

        if (shoppingCartResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(shoppingCartResponse);
    }
}