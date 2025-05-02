using FluentAssertions;
using LudoModels;

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

        var createdPlayer = playerRepository.SavePlayer(new Player { Name = expectedName});

        createdPlayer.Value!.Name.Should().Be(expectedName);
    }

    [TestMethod()]
    public void CanGetPlayers()
    {
        playerRepository.SavePlayer(new Player { Name = "1" });
        playerRepository.SavePlayer(new Player { Name = "2" });

        var players = playerRepository.GetPlayers();

        players.Count().Should().Be(2);
    }

    [TestMethod()]
    public void CanGetPlayerById()
    {
        var expectedPlayer = new Player { Name = "1"};
        var notExpectedPlayer = new Player { Name = "2" };  
        playerRepository.SavePlayer(expectedPlayer);
        playerRepository.SavePlayer(notExpectedPlayer);

        var player = playerRepository.GetPlayer(1)!;


        player.Id.Should().Be(1);
        player.Name.Should().Be(expectedPlayer.Name);
    }

    [TestMethod()]
    public void CanDeletePlayers()
    {
        playerRepository.SavePlayer(new Player { Name = "1" });
        playerRepository.SavePlayer(new Player { Name = "2" });
        var players = playerRepository.GetPlayers();


        playerRepository.DeletePlayer(players.First().Id);


        players = playerRepository.GetPlayers();
        players.Count().Should().Be(1);
    }
}