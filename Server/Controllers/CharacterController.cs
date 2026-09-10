using Closavy.Server.Dtos.Character;
using Closavy.Server.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Closavy.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterController(
    ILogger<CharacterController> logger,
    ICharacterService characterService
) : Controller
{
    private readonly ILogger<CharacterController> _logger = logger;

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostCharacter(CharacterCreateDto createDto, CancellationToken ct = default)
    {
        var createdId = await characterService.CreateAsync(createDto, ct);

        return Created(new Uri(Request.GetEncodedUrl()+ "/" + createdId), createdId);
    }
}