using FluentAssertions;
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

        playerRepo.SavePlayer("john");
        playerRepo.SavePlayer("jane");
    }

    [TestMethod()]
    public void CreateGameTest()
    {
        var player = playerRepo.GetPlayers().First();

        var game = gameService.CreateGame(player.Id);

        game.Value!.players.Should().HaveCount(1).And.Subject.First().Id.Should().Be(player.Id);
    }

    [TestMethod()]
    public void GetGameTest()
    {
        Assert.Fail();
    }

    [TestMethod()]
    public void GetGamesTest()
    {
        Assert.Fail();
    }

    [TestMethod()]
    public void JoinGameTest()
    {
        Assert.Fail();
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