using Application.Services.Interface;
using Domain.Dto.CreatedRequest;
using Microsoft.AspNetCore.Mvc;

namespace IT_RunCourseSecondPartAPI.Controllers;

[ApiController]
[Route("[controller]/[action]/{userId:int}")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public IActionResult Add(UserCreate userCreate)
    {
        return Ok(userService.Add(userCreate));
    }

    [HttpGet]
    public IActionResult GetAllUser()
    {
        return Ok(userService.GetAll());
    }

    [HttpGet]
    public IActionResult GetUserByPagination(int page, int pageSize)
    {
        return Ok(userService.GetUserByPagination(page, pageSize));
    }

    [HttpPut]
    public IActionResult UpdateUser(int userId, UserCreate userCreate)
    {
        var userResponse = userService.Update(userId, userCreate);

        if (userResponse is null)
        {
            return NotFound();
        }

        return Ok(userResponse);
    }

    [HttpDelete]
    public IActionResult DeleteUser(int userId)
    {
        var userResponse = userService.Delete(userId);

        if (userResponse is null)
        {
            return NotFound();
        }

        return Ok(userResponse);
    }

    [HttpGet]
    public IActionResult GetById(int userId)
    {
        var userResponse = userService.GetById(userId);

        if (userResponse is null)
        {
            return NotFound();
        }

        return Ok(userResponse);
    }
}