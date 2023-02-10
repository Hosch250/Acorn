using Acorn.ApiContracts;
using Acorn.Application;
using Microsoft.AspNetCore.Mvc;

namespace Acorn.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostApplication postApplication;

    public PostController(IPostApplication postApplication)
    {
        this.postApplication = postApplication;
    }

    [HttpGet]
    public async Task<List<Post>> GetAll()
    {
        return await postApplication.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await postApplication.Get(id);
        if (response is null)
        {
            return StatusCode(404);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<Post?> Create([FromBody] CreatePost post)
    {
        return await postApplication.Create(post);
    }

    [HttpDelete("{id}")]
    public async Task<StatusCodeResult> Delete(Guid id)
    {
        await postApplication.Delete(id);
        return StatusCode(204);
    }

    [HttpPost("{id}/upvote")]
    public async Task<IActionResult> Upvote(Guid id)
    {
        var response = await postApplication.Upvote(id);
        if (response is null)
        {
            return StatusCode(404);
        }

        return Ok(response);
    }

    [HttpPost("{id}/downvote")]
    public async Task<IActionResult> Downvote(Guid id)
    {
        var response = await postApplication.Downvote(id);
        if (response is null)
        {
            return StatusCode(404);
        }

        return Ok(response);
    }
}