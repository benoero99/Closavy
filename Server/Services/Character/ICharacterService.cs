using Closavy.Server.Dtos.Character;

namespace Closavy.Server.Services.Character;

public interface ICharacterService
{
    public Task<CharacterResponseDto> CreateCharacterAsync(CharacterCreateDto request, CancellationToken ct);
    public Task<CharacterResponseDto> GetCharacterAsync(int characterId, CancellationToken ct);

}