using Closavy.Server.Models.Character.Enums;

namespace Closavy.Server.Dtos.Character;

public class CharacterCreateDto
{
    public string Name { get; set; } = string.Empty;
    public RaceEnum Race { get; set; }
    public DeityEnum Deity { get; set; }
    public ClassEnum Class { get; set; }
}
