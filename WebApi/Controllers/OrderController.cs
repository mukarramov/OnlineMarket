using Application.Services.Interface;
using Domain.Dto.CreatedRequest;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_RunCourseSecondPartAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private int UserId => int.Parse(User.Claims.First(i => i.Type == "id").Value);
    private int UserRole => int.Parse(User.Claims.First(i => i.Type == "Role").Value);

    [Authorize(Policy = "AllRoles")]
    [HttpPost]
    public IActionResult Add(OrderCreate orderCreate)
    {
        return Ok(orderService.Add(orderCreate));
    }

    [Authorize(Policy = "OnlyAdmins")]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(orderService.GetAll());
    }

    [Authorize(Policy = "AllRoles")]
    [HttpGet]
    public IActionResult GetOrderByPagination(int page, int pageSize)
    {
        return Ok(orderService.GetOrderByPagination(page, pageSize, UserId));
    }

    [Authorize(Policy = "OnlyAdmins")]
    [HttpPut]
    public IActionResult Update(int id, OrderCreate orderCreate)
    {
        var orderResponse = orderService.Update(id, orderCreate, UserId);

        if (orderResponse is null)
        {
            return NotFound();
        }

        return Ok(orderResponse);
    }

    [Authorize(Policy = "OnlyAdmins")]
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var orderResponse = orderService.Delete(id, UserId);

        if (orderResponse is null)
        {
            return NotFound();
        }

        return Ok(orderResponse);
    }

    [Authorize(Policy = "AllRoles")]
    [HttpGet]
    public IActionResult GetById(int id)
    {
        var orderResponse = orderService.GetById(id);

        if (orderResponse is null)
        {
            return NotFound();
        }

        return Ok(orderResponse);
    }

    [Authorize(Policy = "OnlyClient")]
    [HttpGet]
    public IActionResult GetOrdersBuUserId(int userId)
    {
        return UserId switch
        {
            0 when userId > 0 => Ok(orderService.GetOrdersByUserId(userId, UserRole)),
            > 0 when userId == 0 => Ok(orderService.GetOrdersByUserId(UserId, UserRole)),
            _ => BadRequest()
        };
    }
}