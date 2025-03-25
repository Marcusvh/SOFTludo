using SoftLudoAPI.Services;
using Xunit;
using FluentAssertions;

namespace LudoTest;

public class SetupTests
{
    private readonly SetupService _setup = new();

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    public void SetupService_PlayerCount_ExpectFail(int playerCount)
    {
        var action = () => _setup.PlayerCount = playerCount;
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void SetupService_PlayerCount_ExpectSuccess(int playerCount)
    {
        _setup.PlayerCount = playerCount;
        _setup.PlayerCount.Should().Be(playerCount);
    }

    [Theory]
    [InlineData("red")]
    [InlineData("green")]
    [InlineData("blue")]
    [InlineData("yellow")]
    public void SetupService_ChoseColour_ExpectSuccess(string colour)
    {
        var result = _setup.ChoseColour(colour);
        result.Should().Be(colour);
    }

    [Theory]
    [InlineData("purple")]
    [InlineData("orange")]
    [InlineData("black")]
    [InlineData("white")]
    public void SetupService_ChoseColour_ExpectFail(string colour)
    {
        var action = () => _setup.ChoseColour(colour);
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void SetupService_GetStartingPlayer_ExpectSuccess()
    {
        _setup.GetStartingPlayer().Should().NotBeNull();
    }

    [Fact]
    public void SetupService_GetStartingPlayer_ExpectFail()
    {
        var action = () => _setup.GetStartingPlayer();
        action.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void SetupService_Setup_ExpectSuccess(int playerCount)
    {
        var result = _setup.Setup(playerCount);
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    public void SetupService_Setup_ExpectFail(int playerCount)
    {
        var action = () => _setup.Setup(playerCount);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}