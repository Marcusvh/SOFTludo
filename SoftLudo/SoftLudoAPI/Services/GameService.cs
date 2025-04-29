using LudoModels;
using SoftLudoApi.Repositories;
using SoftLudoApi.Services;

namespace SoftLudoAPI.Services;

public class GameService : IGameService
{
    private readonly IGameRepository gameRepository; 
    private readonly IPlayerService playerService;
    public GameService(IGameRepository gameRepository, IPlayerService playerService) 
    {
        this.gameRepository = gameRepository; 
        this.playerService = playerService;
    }

    public Result<Game> CreateGame(int playerId)
    {

        var playerResult = playerService.GetPlayer(playerId);

        if (playerResult.Failure)
        {
            return new Result<Game>(ErrorType.PlayerNotFound);
        }

        var game = gameRepository.CreateGame(playerResult.Value!);

        return new Result<Game>(game);
    }

    public Result<Game> GetGame(int id)
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

}
