namespace LudoModels;

public class Game
{
    public int Id { get; set; }
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Player> Rankings { get; set; } = new List<Player>();
    private readonly IDice dice;
    public Player Host { get; set; } = null!;
    public State State { get; private set; }




    public Player? CurrentPlayer { get; set; }
    private int nextCommandId = 1;
    public IEnumerable<Command> Commands { get => GenerateCommands(); }

    public IEnumerable<Command> GenerateCommands()
    {
        return new List<Command>
        {
            new Command
            {
                GameId = Id,
                Id = nextCommandId++,
                PlayerId = 1,//CurrentPlayer!.Id,
                Type = CommandType.Roll
            }
        };
    }


    public Game()
    {
        State = State.Lobby;
        dice = new Dice(1, 6);
    }

    private Player DetermineStartingPlayer(IEnumerable<Player> players)
    {
        foreach (var player in players)
        {
            player.LatestRoll = dice.Roll();
        }

        var highestRoll = players.Max(p => p.LatestRoll);
        var tiedRolls = players.Where(p => p.LatestRoll == highestRoll).ToList();

        while (tiedRolls.Any())
        {
            foreach (var player in players)
            {
                player.LatestRoll = dice.Roll();
            }
            highestRoll = players.Max(p => p.LatestRoll);
            tiedRolls = players.Where(p => p.LatestRoll == highestRoll).ToList();
        }

        return tiedRolls.First();
    }
    public int RollDice(int playerId)
    {
        return dice.Roll();
    }
}