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
        return Ok(orderItemService.Add(orderItemRequest));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(orderItemService.GetAll());
    }
    
    [HttpGet]
    public IActionResult GetOrderItemByPagination(int page, int pageSize)
    {
        return Ok(orderItemService.GetOrderItemByPagination(page, pageSize));
    }

    [HttpPut]
    public IActionResult Update(Guid id, OrderItemCreate orderItemRequest)
    {
        var orderItemResponse = orderItemService.Update(id, orderItemRequest);

        if (orderItemResponse is null)
        {
            return NotFound();
        }

        return Ok(orderItemResponse);
    }

    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        var orderItemResponse = orderItemService.Delete(id);

        if (orderItemResponse is null)
        {
            return NotFound();
        }

        return Ok(orderItemResponse);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var orderItemResponse = orderItemService.GetById(id);

        if (orderItemResponse is null)
        {
            return NotFound();
        }

        return Ok(orderItemResponse);
    }
}