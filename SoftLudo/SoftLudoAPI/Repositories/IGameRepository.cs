using LudoModels;

namespace SoftLudoApi.Repositories;

public interface IGameRepository
{
    Game? GetGame(int id);
    IEnumerable<Game> GetGames();
    Game CreateGame(int hostingPlayerId);
    Game UpdateGame(Game game);
}
