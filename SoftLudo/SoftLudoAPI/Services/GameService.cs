﻿using LudoModels;
using SoftLudoApi.Repositories;
using SoftLudoApi.Services;

namespace SoftLudoAPI.Services;

public class GameService : IGameService
{
    private readonly IGameRepository gameRepository; 
    private readonly IPlayerService playerService;
    public GameService(IGameRepository gameRepository, 
        IPlayerService playerService) 
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

        if (playerResult.Value!.CurrentGameId.HasValue)
        {
            return new Result<Game>(ErrorType.PlayerAlreadyInGame);
        }

        var game = gameRepository.CreateGame(playerResult.Value!);
        playerResult.Value!.CurrentGameId = game.Id;

        return new Result<Game>(game);
    }

    public Result<Game> GetGame(int id)
    {
        var game = gameRepository.GetGame(id);

        if (game == null)
        {
            return new Result<Game>(ErrorType.GameNotFound);
        }

        return new Result<Game>(game);
    }

    public IEnumerable<Game> GetGames()
    {
        return gameRepository.GetGames();
    }

    public Result<Game> JoinGame(int playerId, int gameId)
    {
        var game = gameRepository.GetGame(gameId);

        if (game == null)
        {
            return new Result<Game>(ErrorType.GameNotFound);
        }

        var playerResult = playerService.GetPlayer(playerId);

        if (playerResult.Failure)
        {
            return new Result<Game>(ErrorType.PlayerNotFound);
        }

        if (playerResult.Value!.CurrentGameId.HasValue)
        {
            return new Result<Game>(ErrorType.PlayerAlreadyInGame);
        }

        playerResult.Value!.CurrentGameId = game.Id;
        game.Players.Add(playerResult.Value!);
       
        var updatedGame = gameRepository.UpdateGame(game)!;

        return new Result<Game>(updatedGame);
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
        var game = gameRepository.GetGame(gameId);

        if (game == null)
        {
            return new Result<Game>(ErrorType.GameNotFound);
        }

        if (game.Host.Id != playerId)
        {
            return new Result<Game>(ErrorType.Unauthorized);
        }

        if (game.State != State.Lobby)
        {
            return new Result<Game>(ErrorType.NotStartable);
        }

#warning magic number possibly needs to be moved to some sort of config
        const int MINIMUM_AMOUNT_OF_PLAYERS = 2;
        if (game.Players.Count() < MINIMUM_AMOUNT_OF_PLAYERS)
        {
            return new Result<Game>(ErrorType.NotEnoughPlayers);
        }
#warning move the gamestate running to another class?
        //game.State = GameState.Running;
        var result = gameRepository.UpdateGame(game)!;
        return new Result<Game>(result);
    }
}
