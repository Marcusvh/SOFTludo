namespace LudoModels;

public class Game
{
    public readonly IEnumerable<Player> players = new List<Player>();

    public readonly IEnumerable<Player> rankings = new List<Player>();

}

