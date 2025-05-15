using LudoModels;

namespace SoftLudoApi.Repositories;

public class InMemoryPlayerRepo : IPlayerRepository
{
    private int nextId = 1;
    private readonly List<Player> players = new List<Player>();

    public InMemoryPlayerRepo()
    {
        SavePlayer(new Player { Name = "Per" });
        SavePlayer(new Player { Name = "Sven" });
    }

    public bool DeletePlayer(int id)
    {
        var amountDeleted = players.RemoveAll(p => p.Id == id);
        return amountDeleted > 0;
    }

    public Player? GetPlayer(int id)
    {
        return players.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Player> GetPlayers()
    {
        return players;
    }

    public Result<Player> SavePlayer(Player player)
    {
        if (!ValidateUsername(player.Name))
        {
            return new Result<Player>(ErrorType.InvalidUsername);
        }

        var newPlayer = new Player
        {
            Id = nextId++,
            Name = player.Name,
        };

        players.Add(newPlayer); 

        return new Result<Player>(newPlayer);
    }

    private bool ValidateUsername(string username)
    {
        return !players.Any(p => p.Name == username);
    }
}
