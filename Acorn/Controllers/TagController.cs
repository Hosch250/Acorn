using Acorn.ApiContracts;
using Acorn.Application;
using Microsoft.AspNetCore.Mvc;

namespace Acorn.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagApplication tagApplication;

    public TagController(ITagApplication tagApplication)
    {
        this.tagApplication = tagApplication;
    }

    [HttpGet]
    public async Task<List<Tag>> GetAll()
    {
        return await tagApplication.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await tagApplication.Get(id);
        if (response is null)
        {
            return StatusCode(404);
        }

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditTag tag)
    {
        var response = await tagApplication.Edit(tag);
        if (response is null)
        {
            return StatusCode(404);
        }

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<StatusCodeResult> Delete(Guid id)
    {
        await tagApplication.Delete(id);
        return StatusCode(204);
    }
}
