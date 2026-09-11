namespace Closavy.Server.Models;

public class CharacterEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public int Experience { get; set; }
    public DateTime CreatedAt { get; set; }
    public int AccountId { get; set; }
    public AccountEntity Account { get; set; } = null!;
}
