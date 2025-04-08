using FluentAssertions;

namespace SoftLudoApi.Repositories.Tests;

[TestClass()]
public class InMemoryPlayerRepoTests
{
    private IPlayerRepository playerRepository = new InMemoryPlayerRepo();

    [TestInitialize]
    public void Setup()
    {
        playerRepository = new InMemoryPlayerRepo();
    }


    [TestMethod()]
    public void CanCreatePlayer()
    {
        var expectedName = "johndoe";

        var createdPlayer = playerRepository.SavePlayer(expectedName);

        createdPlayer.Name.Should().Be(expectedName);
    }

    [TestMethod()]
    public void CanGetPlayers()
    {

        playerRepository.SavePlayer("1");
        playerRepository.SavePlayer("2");

        var players = playerRepository.GetPlayers();

        players.Count().Should().Be(2);
    }

    [TestMethod()]
    public void CanGetPlayerById()
    {
        var expectedName = "1";
        var notExpectedName = "2";  
        playerRepository.SavePlayer(expectedName);
        playerRepository.SavePlayer(notExpectedName);

        var player = playerRepository.GetPlayer(1);


        player.Id.Should().Be(1);
        player.Name.Should().Be(expectedName);
    }

    [TestMethod()]
    public void CanDeletePlayers()
    {
        playerRepository.SavePlayer("1");
        playerRepository.SavePlayer("2");
        var players = playerRepository.GetPlayers();


        playerRepository.DeletePlayer(players.First().Id);


        players = playerRepository.GetPlayers();
        players.Count().Should().Be(1);
    }




}