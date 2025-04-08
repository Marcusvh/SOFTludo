using LudoModels;

namespace SoftLudoAPI.Services;

public interface IGameService
{
    Game GetGame(int id);
    IEnumerable<Game> GetGames();
    Game? CreateGame(int userId);
    Game? JoinGame(int userId, int gameId);
    Game? StartGame(int userId, int gameId);
    Game? Roll(int playerId, int gameId);
    Game? PlayTurn(int playerId, int gameId, Command command);
}
