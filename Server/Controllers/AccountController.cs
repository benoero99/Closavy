using Closavy.Server.Dtos.Account;
using Closavy.Server.Services.Account;
using FluentValidation;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Closavy.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(
    ILogger<AccountController> logger,
    IValidator<AccountCreateDto> validator,
    IAccountService accountService
) : Controller
{


    [HttpPost]
    [ProducesResponseType(typeof(AccountResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AccountResponseDto>> PostAccount(AccountCreateDto createDto, CancellationToken ct = default)
    {
        var validationResult = await validator.ValidateAsync(createDto, ct);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        AccountResponseDto createdAccount = await accountService.CreateAccountAsync(createDto, ct);

        return Created(new Uri(Request.GetEncodedUrl() + "/" + createdAccount.Id), createdAccount);
    }

    [HttpGet("{accountId:int}")]
    [ProducesResponseType(typeof(AccountResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AccountResponseDto>> GetCharacter([FromRoute] int accountId, CancellationToken ct = default)
    {
        AccountResponseDto account = await accountService.GetAccountAsync(accountId, ct);

        return Ok(account);
    }
}