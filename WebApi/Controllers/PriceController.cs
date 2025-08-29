using Application.Services.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_RunCourseSecondPartAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PriceController(IPriceService priceService) : ControllerBase
{
    [Authorize(Policy = "OnlyAdmins")]
    [HttpPost]
    public IActionResult Price(double price)
    {
        priceService.Price(price);

        return this.Ok(price);
    }
}
