using LudoModels;

namespace SoftLudoApi.Repositories;

public interface IPlayerRepository
{
    Player? GetPlayer(int id);
    IEnumerable<Player> GetPlayers();
    Player SavePlayer(string username);
    bool DeletePlayer(int id);
}
