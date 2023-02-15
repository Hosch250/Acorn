using Acorn.ApiContracts;
using Acorn.Application;
using Microsoft.AspNetCore.Mvc;

namespace Acorn.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryApplication categoryApplication;

    public CategoryController(ICategoryApplication categoryApplication)
    {
        this.categoryApplication = categoryApplication;
    }

    [HttpGet]
    public async Task<List<Category>> GetAll()
    {
        return await categoryApplication.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await categoryApplication.Get(id);
        if (response is null)
        {
            return StatusCode(404);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<Category> Create([FromBody] CreateCategory category)
    {
        return await categoryApplication.Create(category);
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] Category category)
    {
        var response = await categoryApplication.Edit(category);
        if (response is null)
        {
            return StatusCode(404);
        }

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<StatusCodeResult> Delete(Guid id)
    {
        await categoryApplication.Delete(id);
        return StatusCode(204);
    }
}