using LudoModels;

namespace SoftLudoApi.Repositories;

public class InMemoryPlayerRepo : IPlayerRepository
{

    private readonly List<Player> players = new List<Player>();

    public void DeletePlayer(int id)
    {
        throw new NotImplementedException();
    }

    public Player GetPlayer(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Player> GetPlayers()
    {
        throw new NotImplementedException();
    }

    public Player SavePlayer(string username)
    {
        throw new NotImplementedException();
    }
}
