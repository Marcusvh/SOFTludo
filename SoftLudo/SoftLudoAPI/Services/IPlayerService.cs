using LudoModels;

namespace SoftLudoApi.Services;

public interface IPlayerService
{
    Result<Player> GetPlayer(int id);
    IEnumerable<Player> GetPlayers();
    Result<Player> CreatePlayer(Player player);
    Result<bool> DeletePlayer(int id);
}
