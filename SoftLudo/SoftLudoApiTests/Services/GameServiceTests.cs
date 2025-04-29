using FluentAssertions;
using SoftLudoApi.Repositories;
using SoftLudoApi.Services;

namespace SoftLudoAPI.Services.Tests;

[TestClass()]
public class GameServiceTests
{

    private IGameService gameService = new GameService();
    private IPlayerRepository playerRepo = new InMemoryPlayerRepo();

    [TestInitialize]
    public void Setup()
    {
        gameService = new GameService();
        playerRepo = new InMemoryPlayerRepo();
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