using System;
using LudoModels;
using Reqnroll;
using SoftLudoAPI.Services;

namespace SoftLudoApiTests;

[Binding]
public class GameManagementStepDefinitions
{

    public IGameService GameService = new GameService();
    public Player Player = null!;
    public Game Game = null!;

    [Given("you have a player with id {int}")]
    public void GivenYouHaveAPlayerWithId(int id)
    {
        Player = new Player { Id = id };
    }

    [When("user creates a game")]
    public void WhenUserCreatesAGame()
    {
        Game = GameService.CreateGame(Player.Id)!;
    }

    [Then("the game is created")]
    public void ThenTheGameIsCreated()
    {
        Assert.IsNotNull(Game);
    }
}
