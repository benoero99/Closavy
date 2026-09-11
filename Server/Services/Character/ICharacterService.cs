using Closavy.Server.Dtos.Character;
using Closavy.Server.Models;

namespace Closavy.Server.Services.Character;

public interface ICharacterService
{
    public Task<int> CreateCharacterAsync(CharacterCreateDto request, CancellationToken ct);
    public Task<CharacterResponseDto> GetCharacterAsync(int characterId, CancellationToken ct);

}