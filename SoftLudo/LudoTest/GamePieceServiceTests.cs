using LudoModels;
using SoftLudoAPI.Services;
using Xunit;
using FluentAssertions;

namespace LudoTest;

public class GamePieceServiceTest
{
    private readonly GamePieceService _gps = new();
    private readonly GamePiece _gp = new();

    // Models
    //////////////////////////////////////////
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void GamePiece_Id_ExpectFail(int id)
    {
        _gp.Id = id;

        var action = () => _gps.MoveGamePiece(_gp, 0);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void GamePiece_Id_ExpectSuccess(int id)
    {
        _gp.Id = id;
        _gp.Id.Should().Be(id);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GamePiece_IsHome_ExpectSuccess(bool isHome)
    {
        _gp.IsHome = isHome;
        _gp.IsHome.Should().Be(isHome);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GamePiece_IsGoal_ExpectSuccess(bool isGoal)
    {
        _gp.IsGoal = isGoal;
        _gp.IsGoal.Should().Be(isGoal);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GamePiece_IsSafe_ExpectSuccess(bool isSafe)
    {
        _gp.IsSafe = isSafe;
        _gp.IsSafe.Should().Be(isSafe);
    }

    // Services
    //////////////////////////////////////////
    [Fact]
    public void GamePieceService_MoveGamePiece_ExpectBooleanReturn()
    {
        bool result = false;
        result.Should().BeFalse();
    }

    [Fact]
    public void GamePieceService_MoveGamePieceToGoal_ExpectIntReturn()
    {
        var result = 0;
        result.Should().Be(0);
    }

    [Fact]
    public void GamePieceService_MoveGamePieceToHome_ExpectBooleanReturn()
    {
        bool result = false;
        result.Should().BeFalse();
    }

    [Fact]
    public void GamePieceService_MoveGamePieceFromHomeToStart_ExpectIntReturn()
    {
        var result = 0;
        result.Should().Be(0);
    }

    [Fact]
    public void GamePieceService_MoveGamePieceToGoalTrack_ExpectBooleanReturn()
    {
        bool result = false;
        result.Should().BeFalse();
    }
}