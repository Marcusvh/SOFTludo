namespace LudoModels;

public class Game
{
    public int Id { get; set; }
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Player> Rankings { get; set; } = new List<Player>();
    public Player Host { get; set; } = null!;
    public GameState State { get; set; }
    public int? CurrentPlayerId { get; set; }

    private readonly Random rng = new();

    public void RollForStart()
    {
        if (Players.Count < 2)
        {
            throw new InvalidOperationException("Not enough players to start.");
        }

        foreach (var player in Players)
        {
            player.StartRoll = rng.Next(1, 7);
        }

        var highestRoll = Players.Max(p => p.StartRoll);
        var topRollPlayers = Players.Where(p => p.StartRoll == highestRoll).ToList();

        if (topRollPlayers.Count > 1)
        {
            throw new InvalidOperationException("Tie in start roll. Reroll required.");
        }

        CurrentPlayerId = topRollPlayers.First().Id;
    }
    public int RollDice(int playerId)
    {
        if (State != GameState.Running)
            throw new InvalidOperationException("Game is not running.");

        if (playerId != CurrentPlayerId)
            throw new InvalidOperationException("Not this player's turn.");

        return rng.Next(1, 7);
    }
}