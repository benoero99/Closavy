namespace Closavy.Server.Models;

public class AccountEntity
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public ICollection<CharacterEntity> Characters { get; set; } = [];
}