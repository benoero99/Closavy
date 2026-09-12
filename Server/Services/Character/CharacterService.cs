using Closavy.Server.Data;
using Closavy.Server.Dtos.Character;
using Closavy.Server.Models;
using Closavy.Server.Services.Account;
using Microsoft.EntityFrameworkCore;

namespace Closavy.Server.Services.Character;

public class CharacterService(
    GameDbContext dbContext,
    ICurrentAccountService currentAccountService
) : ICharacterService
{
    public async Task<CharacterResponseDto> CreateCharacterAsync(CharacterCreateDto request, CancellationToken ct)
    {
        if (await IsCharacterNameAlreadyInUse(request.Name, ct))
            throw new Exception("Character name is already in use!");

        var accountId = currentAccountService.GetLoggedInUser();

        CharacterEntity character = new()
        {
            AccountId = accountId,
            Name = request.Name,
            Level = 0,
            Experience = 0,
            CreatedAt = DateTime.UtcNow,
        };

        await dbContext.AddAsync(character, ct);
        await dbContext.SaveChangesAsync(ct);

        return new CharacterResponseDto
        {
            Id = character.Id,
            Name = character.Name,
            Level = character.Level,
            Experience = character.Experience,
            CreatedAt = character.CreatedAt,
        };
    }

    public async Task<CharacterResponseDto> GetCharacterAsync(int characterId, CancellationToken ct)
    {
        CharacterEntity? character = await dbContext.Characters.FindAsync([characterId], cancellationToken: ct) ?? throw new Exception("Character not found");
        
        return new CharacterResponseDto
        {
            Id = character.Id,
            Name = character.Name,
            Level = character.Level,
            Experience = character.Experience,
            CreatedAt = character.CreatedAt,
        };
    }

    public async Task<List<CharacterResponseDto>> GetCharactersByAccountIdAsync(int accountId, CancellationToken ct)
    {
        List<CharacterEntity> characters = await dbContext.Characters.Where(c => c.AccountId == accountId).ToListAsync(ct);
        List<CharacterResponseDto> characterResponseDtos = [];
        foreach (var character in characters)
        {
            characterResponseDtos.Add(new CharacterResponseDto
            {
                Id = character.Id,
                Name = character.Name,
                Level = character.Level,
                Experience = character.Experience,
                CreatedAt = character.CreatedAt,
            });
        }
        
        return characterResponseDtos;
    }


    private async Task<bool> IsCharacterNameAlreadyInUse(string name, CancellationToken ct)
    {
        CharacterEntity? character = await dbContext.Characters.SingleOrDefaultAsync(c => c.Name == name, ct);
        return character != null;
    }
}
