using Closavy.Server.Data;
using Closavy.Server.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace Closavy.Server.Services.Character;

public class CharacterService(
    GameDbContext dbContext
) : ICharacterService
{
    public async Task<int> CreateAsync(CharacterCreateDto request, CancellationToken ct)
    {
        if (await IsCharacterNameAlreadyInUse(request.Name))
            throw new Exception("Character name is already in use!");
        Models.Character character = new()
        {
            Name = request.Name,
            Level = 0,
            Experience = 0,
            CreatedAt = DateTime.UtcNow,
        };

        await dbContext.AddAsync(character, ct);
        await dbContext.SaveChangesAsync(ct);

        return character.Id;
    }

    private async Task<bool> IsCharacterNameAlreadyInUse(string name)
    {
        Models.Character? character = await dbContext.Characters.SingleOrDefaultAsync(c => c.Name == name);
        return character != null;
    }
}