using Application.Services.Interface;
using Domain.Dto.CreatedRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_RunCourseSecondPartAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [Authorize(Policy = "OnlyAdmins")]
    [HttpPost]
    public IActionResult Add(CategoryCreate categoryCreate)
    {
        return this.Ok(categoryService.Add(categoryCreate));
    }

    [Authorize(Policy = "AllRoles")]
    [HttpGet]
    public IActionResult GetAll()
    {
        return this.Ok(categoryService.GetAll());
    }

    [Authorize(Policy = "AllRoles")]
    [HttpGet]
    public IActionResult GetCategoryByPagination(int page, int pageSize)
    {
        return this.Ok(categoryService.GetCategoryByPagination(page, pageSize));
    }

    [Authorize(Policy = "OnlyAdmins")]
    [HttpPut]
    public IActionResult Update(int id, CategoryCreate categoryCreate)
    {
        var categoryResponse = categoryService.Update(id, categoryCreate);

        if (categoryResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(categoryResponse);
    }

    [Authorize(Policy = "OnlyAdmins")]
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var categoryResponse = categoryService.Delete(id);

        if (categoryResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(categoryResponse);
    }

    [Authorize(Policy = "AllRoles")]
    [HttpGet]
    public IActionResult GetById(int id)
    {
        var categoryResponse = categoryService.GetById(id);

        if (categoryResponse is null)
        {
            return this.NotFound();
        }

        return this.Ok(categoryResponse);
    }
}
