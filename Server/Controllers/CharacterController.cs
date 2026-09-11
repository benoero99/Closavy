using Closavy.Server.Dtos.Character;
using Closavy.Server.Models;
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
    private readonly ILogger<CharacterController> _logger = logger;
    private readonly IValidator<CharacterCreateDto> _validator = validator;

    [HttpPost]
    [ProducesResponseType(typeof(CharacterResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CharacterResponseDto>> PostCharacter(CharacterCreateDto createDto, CancellationToken ct = default)
    {
        var validationResult = await _validator.ValidateAsync(createDto, ct);

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
}