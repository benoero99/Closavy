using Closavy.Server.Data;
using Closavy.Server.Dtos.Character;
using Closavy.Server.Exceptions;
using Closavy.Server.Models.Character;
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
            throw new NameAlreadyTakenException($"Character name '{request.Name}' is already in use!");

        var accountId = currentAccountService.GetLoggedInUser();

        if (accountId == 0)
        {
            throw new Exception("No logged in user");
        }

        CharacterEntity character = new()
        {
            AccountId = accountId,
            Name = request.Name,
            Class = request.Class,
            Deity = request.Deity,
            Race = request.Race,
            Level = 0,
            Experience = 0,
            CreatedAt = DateTime.UtcNow,
        };

        await dbContext.AddAsync(character, ct);
        await dbContext.SaveChangesAsync(ct);

        return CreateCharacterResponseDto(character);
    }

    public async Task<CharacterResponseDto> GetCharacterAsync(int characterId, CancellationToken ct)
    {
        CharacterEntity? character = await dbContext.Characters.FindAsync([characterId], cancellationToken: ct) ?? throw new EntityNotFoundException($"Character with id {characterId} not found");

        return CreateCharacterResponseDto(character);
    }

    public async Task<List<CharacterResponseDto>> GetCharactersByAccountIdAsync(int accountId, CancellationToken ct)
    {
        List<CharacterEntity> characters = await dbContext.Characters.Where(c => c.AccountId == accountId).ToListAsync(ct);
        List<CharacterResponseDto> characterResponseDtos = [];
        foreach (var character in characters)
        {
            characterResponseDtos.Add(CreateCharacterResponseDto(character));
        }

        return characterResponseDtos;
    }

    public async Task<List<CharacterResponseDto>> GetCharactersByLoggedInUserAsync(CancellationToken ct)
    {
        var currentUserId = currentAccountService.GetLoggedInUser();
        List<CharacterEntity> characters = await dbContext.Characters.Where(c => c.AccountId == currentUserId).ToListAsync(ct);
        List<CharacterResponseDto> characterResponseDtos = [];
        foreach (var character in characters)
        {
            characterResponseDtos.Add(CreateCharacterResponseDto(character));
        }

        return characterResponseDtos;
    }

    private async Task<bool> IsCharacterNameAlreadyInUse(string name, CancellationToken ct)
    {
        CharacterEntity? character = await dbContext.Characters.SingleOrDefaultAsync(c => c.Name == name, ct);
        return character != null;
    }

    private static CharacterResponseDto CreateCharacterResponseDto(CharacterEntity entity)
    {
        return new CharacterResponseDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Class = entity.Class,
            Deity = entity.Deity,
            Race = entity.Race,
            Level = entity.Level,
            Experience = entity.Experience,
            CreatedAt = entity.CreatedAt,
        };
    }
}
