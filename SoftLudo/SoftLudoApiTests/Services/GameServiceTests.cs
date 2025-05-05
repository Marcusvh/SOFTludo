using FluentAssertions;
using LudoModels;
using SoftLudoApi.Repositories;
using SoftLudoApi.Services;

namespace SoftLudoAPI.Services.Tests;

[TestClass()]
public class GameServiceTests
{

    private IPlayerService playerService = null!;
    private IGameService gameService = null!;
    private IPlayerRepository playerRepo = null!;
    private IGameRepository gameRepo = null!;

    [TestInitialize]
    public void Setup()
    {
        playerRepo = new InMemoryPlayerRepo();
        gameRepo = new InMemoryGameRepo();
        playerService = new PlayerService(playerRepo);
        gameService = new GameService(gameRepo, playerService);

        playerRepo.SavePlayer(new Player { Name = "John" });
        playerRepo.SavePlayer(new Player { Name = "Jane" });
    }

    [TestMethod()]
    public void CreateGameTest()
    {
        var player = playerRepo.GetPlayers().First();

        var game = gameService.CreateGame(player.Id);

        game.Value!.Players.Should().HaveCount(1).And.Subject.First().Id.Should().Be(player.Id);
    }

    [TestMethod()]
    public void GetGameTest()
    {
        var player = playerRepo.GetPlayers().First();
        var createdGame = gameRepo.CreateGame(player);

        var game = gameService.GetGame(createdGame.Id);

        game.Success.Should().BeTrue();
        game.Value!.Id.Should().Be(createdGame.Id);
        
    }

    [TestMethod()]
    public void GetGamesTest()
    {
        var player = playerRepo.GetPlayers().First();
        gameRepo.CreateGame(player);
        gameRepo.CreateGame(player);

        var games = gameService.GetGames();

        games.Count().Should().Be(2); 
    }

    [TestMethod()]
    public void JoinGameTest()
    {
        var firstPlayer = playerRepo.GetPlayers().First();
        var secondPlayer = playerRepo.GetPlayers().Last();
        var players = new List<Player> { firstPlayer, secondPlayer };
        var game = gameService.CreateGame(firstPlayer.Id).Value!;

        var result = gameService.JoinGame(secondPlayer.Id, game.Id);

        result.Success.Should().BeTrue();
        result.Value!.Players.Should().Contain(players);
    }

    [TestMethod()]
    public void PlayTurnTest()
    {
        Assert.Fail();
    }

    [TestMethod()]
    public void RollTest()
    {
        Assert.Fail();
    }

    [TestMethod()]
    public void StartGameTest()
    {
        Assert.Fail();
    }
}