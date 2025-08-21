using Application.Services.Interface;
using Domain.Dto.CreatedRequest;
using Microsoft.AspNetCore.Mvc;

namespace IT_RunCourseSecondPartAPI.Controllers;

[ApiController]
[Route("[controller][action]")]
public class OrderItemController(IOrderItemService orderItemService) : ControllerBase
{
    [HttpPost]
    public IActionResult Add(OrderItemCreate orderItemRequest)
    {
        return this.Ok(orderItemService.Add(orderItemRequest));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return this.Ok(orderItemService.GetAll());
    }
    
    [HttpGet]
    public IActionResult GetOrderItemByPagination(int page, int pageSize)
    {
        return this.Ok(orderItemService.GetOrderItemByPagination(page, pageSize));
    }

    [HttpPut]
    public IActionResult Update(int id, OrderItemCreate orderItemRequest)
    {
        var orderItemResponse = orderItemService.Update(id, orderItemRequest);

        if (orderItemResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(orderItemResponse);
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var orderItemResponse = orderItemService.Delete(id);

        if (orderItemResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(orderItemResponse);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var orderItemResponse = orderItemService.GetById(id);

        if (orderItemResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(orderItemResponse);
    }
}