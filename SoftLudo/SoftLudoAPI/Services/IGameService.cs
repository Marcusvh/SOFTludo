using LudoModels;

namespace SoftLudoAPI.Services;

public interface IGameService
{
    Game GetGame(int id);
    IEnumerable<Game> GetGames();
    Game? CreateGame(int playerId);
    Game? JoinGame(int playerId, int gameId);
    Game? StartGame(int playerId, int gameId);
    Game? Roll(int playerId, int gameId);
    Game? PlayTurn(int playerId, int gameId, Command command);
}
