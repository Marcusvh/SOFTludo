using FluentAssertions;
using LudoModels;
using Moq;
using SoftLudoApi.Repositories;
using SoftLudoApi.Services;

namespace SoftLudoAPI.Services.Tests;

[TestClass]
public class GameServiceTests
{
    private Mock<IPlayerService> playerServiceMock = null!;
    private Mock<IGameRepository> gameRepoMock = null!;
    private GameService gameService = null!;

    private Player player1 = new() { Id = 1, Name = "John" };
    private Player player2 = new() { Id = 2, Name = "Jane" };

    private Game testGame = null!;

    [TestInitialize]
    public void Setup()
    {
        playerServiceMock = new Mock<IPlayerService>();
        gameRepoMock = new Mock<IGameRepository>();
        gameService = new GameService(gameRepoMock.Object, playerServiceMock.Object);

        testGame = new Game { Id = 100 };
        testGame.Players.Add(player1);

        playerServiceMock.Setup(s => s.GetPlayer(1))
                         .Returns(new Result<Player>(player1));
        playerServiceMock.Setup(s => s.GetPlayer(2))
                         .Returns(new Result<Player>(player2));

        gameRepoMock.Setup(r => r.GetGame(testGame.Id))
                    .Returns(testGame);
    }

    [TestMethod]
    public void CreateGameTest()
    {
        // Arrange
        gameRepoMock.Setup(r => r.CreateGame(player1))
                    .Returns((Player host) =>
                    {
                        var game = new Game { Id = 200 };
                        game.Players.Add(host);
                        return game;
                    });

        // Act
        var result = gameService.CreateGame(player1.Id);

        // Assert
        result.Success.Should().BeTrue();
        result.Value!.Players.Should().HaveCount(1);
        result.Value.Players.First().Id.Should().Be(player1.Id);
    }

    [TestMethod]
    public void GetGameTest()
    {
        // Act
        var result = gameService.GetGame(testGame.Id);

        // Assert
        result.Success.Should().BeTrue();
        result.Value!.Id.Should().Be(testGame.Id);
    }

    [TestMethod]
    public void GetGamesTest()
    {
        // Arrange
        var game1 = new Game { Id = 1 };
        game1.Players.Add(player1);
        var game2 = new Game { Id = 2 };
        game2.Players.Add(player2);

        gameRepoMock.Setup(r => r.GetGames()).Returns(new List<Game> { game1, game2 });

        // Act
        var result = gameService.GetGames();

        // Assert
        result.Should().HaveCount(2);
    }

    [TestMethod]
    public void JoinGameTest()
    {
        // Arrange
        gameRepoMock.Setup(r => r.UpdateGame(It.IsAny<Game>()))
                    .Returns<Game>(g => g); 

        // Act
        var result = gameService.JoinGame(player2.Id, testGame.Id);

        // Assert
        result.Success.Should().BeTrue();
        result.Value!.Players.Should().Contain(p => p.Id == player2.Id);
        result.Value.Players.Should().HaveCount(2);
    }

    [TestMethod]
    public void PlayTurnTest()
    {
        Assert.Fail("Not implemented yet.");
    }

    [TestMethod]
    [DataRow(1, 6, 0)]
    [DataRow(1, 6, 100)]
    [DataRow(1, 6, 999)]
    public void Roll_ShouldReturnValueWithinRange(int min, int max, int seed)
    {
        // Arrange
        var dice = new Dice(min, max, seed);

        // Act
        int result = dice.Roll();

        // Assert
        result.Should().BeGreaterThanOrEqualTo(min).And.BeLessThanOrEqualTo(max);
    }

    [TestMethod]
    public void StartGameTest()
    {
        Assert.Fail("Not implemented yet.");
    }
}
