using Closavy.Server.Dtos.Account;
using Closavy.Server.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Closavy.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(
    ILogger<AuthController> logger,
    IAuthService authService
) : Controller
{
    [HttpPost("Login")]
    [ProducesResponseType(typeof(AccountResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AccountResponseDto>> Login(string displayName, CancellationToken ct)
    {
        AccountResponseDto account = await authService.LoginAsync(displayName, ct);

        return Ok(account);
    }

    [HttpPost("Logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Logout(CancellationToken ct)
    {
        authService.Logout();

        return Ok();
    }
}
