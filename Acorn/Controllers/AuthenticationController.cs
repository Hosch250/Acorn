using Acorn.Domain.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Acorn.Controllers;

[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    public record LoginRequest(string UserName, string Password, string? ReturnUrl);
    public record CreateUserRequest(string UserName, string Password, string PasswordConfirmation, string? ReturnUrl = null);

    private readonly UserManager<User> userManager;

    public AuthenticationController(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var claims = new List<Claim>
        {
            new Claim("user", loginRequest.UserName),
        };

        var user = await userManager.FindByNameAsync(loginRequest.UserName);
        if (user is null)
        {
            return BadRequest();
        }

        if (await userManager.IsLockedOutAsync(user))
        {
            return Unauthorized();
        }

        if (await userManager.CheckPasswordAsync(user, loginRequest.Password))
        {
            await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "token", "user", "role")));

            return Redirect(loginRequest.ReturnUrl ?? "/");
        }
        else
        {
            await userManager.AccessFailedAsync(user);
        }

        return Challenge();
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest createUserRequest)
    {
        if (createUserRequest.Password != createUserRequest.PasswordConfirmation)
        {
            return BadRequest();
        }

        var user = new User
        {
            UserName = createUserRequest.UserName
        };

        var result = await userManager.CreateAsync(user, createUserRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return Redirect(createUserRequest.ReturnUrl ?? "/");
    }
}