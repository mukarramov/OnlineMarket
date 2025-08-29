using Application.Services.Interface;
using Domain.Dto.CreatedRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_RunCourseSecondPartAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private int UserId => int.Parse(this.User.Claims.First(i => i.Type == "id").Value);
    private int UserRole => int.Parse(this.User.Claims.First(i => i.Type == "Role").Value);

    [Authorize(Policy = "AllRoles")]
    [HttpPost]
    public IActionResult Add(OrderCreate orderCreate)
    {
        return this.Ok(orderService.Add(orderCreate));
    }

    [Authorize(Policy = "OnlyAdmins")]
    [HttpGet]
    public IActionResult GetAll()
    {
        return this.Ok(orderService.GetAll());
    }

    [Authorize(Policy = "AllRoles")]
    [HttpGet]
    public IActionResult GetOrderByPagination(int page, int pageSize)
    {
        return this.Ok(orderService.GetOrderByPagination(page, pageSize, this.UserId));
    }

    [Authorize(Policy = "OnlyAdmins")]
    [HttpPut]
    public IActionResult Update(int id, OrderCreate orderCreate)
    {
        var orderResponse = orderService.Update(id, orderCreate, this.UserId);

        if (orderResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(orderResponse);
    }

    [Authorize(Policy = "OnlyAdmins")]
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var orderResponse = orderService.Delete(id, this.UserId);

        if (orderResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(orderResponse);
    }

    [Authorize(Policy = "AllRoles")]
    [HttpGet]
    public IActionResult GetById(int id)
    {
        var orderResponse = orderService.GetById(id);

        if (orderResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(orderResponse);
    }

    [Authorize(Policy = "OnlyClient")]
    [HttpGet]
    public IActionResult GetOrdersBuUserId(int userId)
    {
        return this.UserId switch
        {
            0 when userId > 0 => this.Ok(orderService.GetOrdersByUserId(userId, this.UserRole)),
            > 0 when userId == 0 => this.Ok(orderService.GetOrdersByUserId(this.UserId, this.UserRole)),
            _ => this.BadRequest()
        };
    }
}
