namespace LudoModels;

public class Game
{
    public int Id { get; set; }
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Player> Rankings { get; set; } = new List<Player>();
    private IDice dice { get; set; }
    public Player Host { get; set; } = null!;
    public GameState State { get; set; }
    public int? CurrentPlayerId { get; set; }
    public Game()
    {
        dice = new Dice(1, 6);
    }


    public void DetermineStartingPlayer()
    {
        foreach (var player in Players)
        {
            player.LatestRoll = dice.Roll();
        }

        var highestRoll = Players.Max(p => p.LatestRoll);
        var tiedRollPlayers = Players.Where(p => p.LatestRoll == highestRoll).ToList();

        if (tiedRollPlayers.Count > 1)
        {
            
        }

        CurrentPlayerId = tiedRollPlayers.First().Id;
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