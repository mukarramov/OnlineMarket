using Application.Services.Interface;
using Domain.Dto.CreatedRequest;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IT_RunCourseSecondPartAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Registration(AuthUser user)
    {
        if (user.Role is Role.Admin or Role.SuperAdmin)
        {
            return this.BadRequest("you can not add the admin or superadmin!");
        }

        await userService.Registration(user);

        return this.Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(string? email, string? password)
    {
        if (email is null || password is null)
        {
            return this.BadRequest("email and password is null!");
        }

        var token = await userService.LogIn(email, password);

        if (token == null)
        {
            throw new Exception("not found!");
        }

        return this.Ok(token);
    }

    [HttpGet]
    public IActionResult GetAllUser()
    {
        return this.Ok(userService.GetAll());
    }

    [HttpGet]
    public IActionResult GetUserByPagination(int page, int pageSize)
    {
        return this.Ok(userService.GetUserByPagination(page, pageSize));
    }

    [HttpPut]
    public IActionResult UpdateUser(int userId, UserCreate userCreate)
    {
        var userResponse = userService.Update(userId, userCreate);

        if (userResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(userResponse);
    }

    [HttpDelete]
    public IActionResult DeleteUser(int userId)
    {
        var userResponse = userService.Delete(userId);

        if (userResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(userResponse);
    }

    [HttpGet]
    public IActionResult GetById(int userId)
    {
        var userResponse = userService.GetById(userId);

        if (userResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(userResponse);
    }
}