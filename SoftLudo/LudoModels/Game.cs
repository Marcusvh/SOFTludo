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

        var contenders = new List<Player>(Players);

        while (true)
        {
            foreach (var player in contenders)
            {
                player.StartRoll = rng.Next(1, 7);
            }

            var highestRoll = contenders.Max(p => p.StartRoll);

            contenders = contenders.Where(p => p.StartRoll == highestRoll).ToList();

            if (contenders.Count == 1)
            {
                CurrentPlayerId = contenders[0].Id;
                return;
            }
        }
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