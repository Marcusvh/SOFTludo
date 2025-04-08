using LudoModels;

namespace SoftLudoAPI.Services;

public class GameService : IGameService
{
    public Game? CreateGame(int playerId)
    {
        throw new NotImplementedException();
    }

    public Game GetGame(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Game> GetGames()
    {
        throw new NotImplementedException();
    }

    public Game? JoinGame(int playerId, int gameId)
    {
        throw new NotImplementedException();
    }

    public Game? PlayTurn(int playerId, int gameId, Command command)
    {
        throw new NotImplementedException();
    }

    public Game? Roll(int playerId, int gameId)
    {
        throw new NotImplementedException();
    }

    public Game? StartGame(int playerId, int gameId)
    {
        throw new NotImplementedException();
    }
}
