using Closavy.Server.Dtos.Character;
using Closavy.Server.Services.Account;
using Closavy.Server.Services.Character;
using FluentValidation;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Closavy.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterController(
    ILogger<CharacterController> logger,
    ICharacterService characterService,
    IValidator<CharacterCreateDto> validator
) : Controller
{
    [HttpPost]
    [ProducesResponseType(typeof(CharacterResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CharacterResponseDto>> PostCharacter(CharacterCreateDto createDto, CancellationToken ct = default)
    {
        var validationResult = await validator.ValidateAsync(createDto, ct);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        CharacterResponseDto createdCharacter = await characterService.CreateCharacterAsync(createDto, ct);

        return Created(new Uri(Request.GetEncodedUrl() + "/" + createdCharacter.Id), createdCharacter);
    }

    [HttpGet("{characterId:int}")]
    [ProducesResponseType(typeof(CharacterResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CharacterResponseDto>> GetCharacter([FromRoute] int characterId, CancellationToken ct = default)
    {
        CharacterResponseDto character = await characterService.GetCharacterAsync(characterId, ct);

        return Ok(character);
    }

    [HttpGet("AccountId/{accountId:int}/Characters", Name = "GetCharactersByAccountId")]
    [ProducesResponseType(typeof(List<CharacterResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CharacterResponseDto>> GetCharactersByAccountId([FromRoute] int accountId, CancellationToken ct = default)
    {
        List<CharacterResponseDto> character = await characterService.GetCharactersByAccountIdAsync(accountId, ct);

        return Ok(character);
    }
}
