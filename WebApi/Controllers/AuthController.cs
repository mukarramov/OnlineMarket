using Application.Services.Interface;
using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IT_RunCourseSecondPartAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Registration(AuthUser user)
    {
        if (user.Role is Role.Admin or Role.SuperAdmin)
        {
            return BadRequest("you can not add the admin or superadmin!");
        }

        await authService.Registration(user);
        
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(string? email, string? password)
    {
        if (email is null || password is null)
        {
            NotFound("email and password is null!");
        }

        var token = await authService.LogIn(email, password);

        if (token == null)
        {
            throw new Exception("not found!");
        }

        return Ok(token);
    }
}