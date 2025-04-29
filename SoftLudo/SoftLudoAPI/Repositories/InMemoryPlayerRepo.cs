using LudoModels;

namespace SoftLudoApi.Repositories;

public class InMemoryPlayerRepo : IPlayerRepository
{
    private int nextId = 1;
    private readonly List<Player> players = new List<Player>();

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

    public Player SavePlayer(string username)
    {
        var newPlayer = new Player
        {
            Id = nextId++,
            Name = username,
        };
        players.Add(newPlayer); 
        return newPlayer;
    }
}
