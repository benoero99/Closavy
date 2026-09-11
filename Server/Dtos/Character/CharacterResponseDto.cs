namespace Closavy.Server.Dtos.Character;

public class CharacterResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public int Experience { get; set; }
    public DateTime CreatedAt { get; set; }
}
