namespace Closavy.Server.Models;

public class Character
{
    public int Id { get; set; }   
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }   
    public int Experience { get; set; }   
    public DateTime CreatedAt { get; set; }   
}