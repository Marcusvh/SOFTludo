using System;
using FluentAssertions;
using LudoModels;
using Moq;
using Reqnroll;
using SoftLudoApi.Repositories;
using SoftLudoApi.Services;
using SoftLudoAPI.Services;

namespace SoftLudoApiTests
{
    

    [Binding]
    public class GameManagementStepDefinitions
    {
        private Mock<IPlayerService> playerServiceMock;
        private Mock<IGameRepository> gameRepoMock;
        private GameService gameService;
        private Player? player;
        private Result<Game>? createGameResult;
        public GameManagementStepDefinitions()
        {
            playerServiceMock = new Mock<IPlayerService>();
            gameRepoMock = new Mock<IGameRepository>();
            gameService = new GameService(gameRepoMock.Object, playerServiceMock.Object);
        }

        [Given("you have a player with id {int}")]
        public void GivenYouHaveAPlayerWithId(int id)
        {
            player = new Player { Id = id, Name = "Test Player"};
            playerServiceMock.Setup(s => s.GetPlayer(id))
                .Returns(new Result<Player>(player));
        }

        [Given("you have a player without id")]
        public void GivenYouHaveAPlayerWithoutId()
        {
            player = null;
        }

        [When("user creates a game")]
        public void WhenUserCreatesAGame()
        {
            

            if (player != null)
            {
                gameRepoMock.Setup(r => r.CreateGame(player))
                    .Returns((Player host) =>
                    {
                        var game = new Game { Id = 200 };
                        game.Players.Add(host);
                        return game;
                    });

                createGameResult = gameService.CreateGame(player.Id);
            }
            else
            {
                // Simulate that no player exists for id -1
                playerServiceMock.Setup(s => s.GetPlayer(-1))
                    .Returns(new Result<Player>(ErrorType.PlayerNotFound));


                // Use an invalid id -1, to simulate missing player
                createGameResult = gameService.CreateGame(-1);
            }
        }

        [Then("the game is created")]
        public void ThenTheGameIsCreated()
        {
            createGameResult.Should().NotBeNull("No result from game creation.");
            createGameResult!.Success.Should().BeTrue("Game was not created successfully.");
            createGameResult.Value.Should().NotBeNull("Game object is null.");
        }

        [Then("no game is created and throws error")]
        public void ThenNoGameIsCreatedAndThrowsError()
        {
            createGameResult.Should().NotBeNull("No result from game creation.");
            createGameResult!.Success.Should().BeFalse("Game should not have been created.");
            createGameResult.ErrorType.Should().Be(ErrorType.PlayerNotFound, $"Expected PlayerNotFound error, got {createGameResult.ErrorType}.");
        }
    }
}
