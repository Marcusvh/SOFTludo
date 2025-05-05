namespace LudoModels;

public class Game
{
    public int Id { get; set; } 
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Player> Rankings { get; set; } = new List<Player>();
    public Player Host { get; set; } = null!;
    public GameState State { get; set; }
}