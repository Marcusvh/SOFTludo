using LudoModels;

namespace SoftLudoAPI.Services;

public interface IGameService
{
    Result<Game> GetGame(int id);
    IEnumerable<Game> GetGames();
    Result<Game> CreateGame(int playerId);
    Result<Game> JoinGame(int playerId, int gameId);
    Result<Game> StartGame(int playerId, int gameId);
    Result<Game> Roll(int playerId, int gameId);
    Result<Game> PlayTurn(int playerId, int gameId, Command command);
}
