namespace LudoModels;

public class Game
{
    public int Id { get; set; } 
    public readonly ICollection<Player> players = new List<Player>();
    public readonly ICollection<Player> rankings = new List<Player>();
}