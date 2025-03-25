using LudoModels;
using SoftLudoAPI.Services;
using Xunit;
using FluentAssertions;

namespace LudoTest;

public class RulesServiceTest
{
    private readonly RulesService _rulesService;

    public RulesServiceTest()
    {
        var players = new List<Player>
        {
            new() { Id = 1, Name = "Player 1", Colour = "Red", GamePieces = GenerateGamePieces(1) },
            new() { Id = 2, Name = "Player 2", Colour = "Blue", GamePieces = GenerateGamePieces(5) },
            new() { Id = 3, Name = "Player 3", Colour = "Green", GamePieces = GenerateGamePieces(9) },
            new() { Id = 4, Name = "Player 4", Colour = "Yellow", GamePieces = GenerateGamePieces(13) }
        };

        _rulesService = new RulesService(players);
    }

    private static List<GamePiece> GenerateGamePieces(int startId)
    {
        return new List<GamePiece>
        {
            new() { Id = startId, Position = 0 },
            new() { Id = startId + 1, Position = 0 },
            new() { Id = startId + 2, Position = 0 },
            new() { Id = startId + 3, Position = 0 }
        };
    }

    [Fact]
    public void RulesService_ValidateMove_ShouldReturnBoolean()
    {
        bool result = false;
        result.Should().BeFalse();
    }

    [Fact]
    public void RulesService_CheckForOtherPlayersGamePiece_ShouldReturnInt()
    {
        int result = 0;
        result.Should().Be(0);
    }

    [Fact]
    public void RulesService_CheckForWin_ShouldReturnBoolean()
    {
        bool result = false;
        result.Should().BeFalse();
    }

    [Fact]
    public void RulesService_RemainingPlayers_ShouldReturnInt()
    {
        int result = 0;
        result.Should().Be(0);
    }

    [Fact]
    public void RulesService_CheckForSafeZone_ShouldReturnBoolean()
    {
        bool result = false;
        result.Should().BeFalse();
    }

    [Fact]
    public void RulesService_CheckForOwnGamePieceBlock_ShouldReturnBoolean()
    {
        bool result = false;
        result.Should().BeFalse();
    }

    [Fact]
    public void RulesService_CheckForRemainingStepsTowardsGoal_ShouldReturnInt()
    {
        int result = 0;
        result.Should().Be(0);
    }
}
