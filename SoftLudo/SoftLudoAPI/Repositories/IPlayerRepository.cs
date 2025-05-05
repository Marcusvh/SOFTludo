using LudoModels;

namespace SoftLudoApi.Repositories;

public interface IPlayerRepository
{
    Player? GetPlayer(int id);
    IEnumerable<Player> GetPlayers();
    Result<Player> SavePlayer(Player player);
    bool DeletePlayer(int id);
}
