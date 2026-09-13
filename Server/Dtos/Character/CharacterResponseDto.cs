using Closavy.Server.Models.Character.Enums;

namespace Closavy.Server.Dtos.Character;

public class CharacterResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public RaceEnum Race { get; set; }
    public DeityEnum Deity { get; set; }
    public ClassEnum Class { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public DateTime CreatedAt { get; set; }
}
