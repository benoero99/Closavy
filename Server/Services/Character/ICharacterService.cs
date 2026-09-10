using Closavy.Server.Dtos.Character;

namespace Closavy.Server.Services;

public interface ICharacterService
{
    public Task<int> CreateAsync(CharacterCreateDto request, CancellationToken ct);
}