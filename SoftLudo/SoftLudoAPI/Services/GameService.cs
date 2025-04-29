using LudoModels;

namespace SoftLudoAPI.Services;

public class GameService : IGameService
{
    public Result<Game> CreateGame(int playerId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Game> GetGames()
    {
        throw new NotImplementedException();
    }

    public Result<Game> JoinGame(int playerId, int gameId)
    {
        throw new NotImplementedException();
    }

    public Result<Game> PlayTurn(int playerId, int gameId, Command command)
    {
        throw new NotImplementedException();
    }

    public Result<Game> Roll(int playerId, int gameId)
    {
        throw new NotImplementedException();
    }

    public Result<Game> StartGame(int playerId, int gameId)
    {
        throw new NotImplementedException();
    }

    Result<Game> IGameService.GetGame(int id)
    {
        throw new NotImplementedException();
    }
}
