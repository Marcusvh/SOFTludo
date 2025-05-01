namespace LudoModels;

public class Game
{
    public int Id { get; set; } 
    public readonly ICollection<Player> Players = new List<Player>();
    public readonly ICollection<Player> Rankings = new List<Player>();
}