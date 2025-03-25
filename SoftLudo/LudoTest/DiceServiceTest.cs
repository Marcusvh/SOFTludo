using LudoModels;
using SoftLudoAPI.Services;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace LudoTest;

public class DiceServiceTest
{
    private readonly DiceService _diceService = new();

    [Fact]
    public void DiceService_RollDice_NullPlayer_ExpectFail()
    {
        var action = () => _diceService.RollDice(null);
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DiceService_RollDice_ExpectSuccess()
    {
        var player = new Player();

        int result = _diceService.RollDice(player);

        result.Should().BeInRange(1, 6);
    }

    [Fact]
    public void DiceService_IfRollSix_ExpectSuccess()
    {
        var player = new Player();

        _diceService.IfRollSix(player);

        // TODO: Implement actual logic
        true.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(GetRollForStartingPlayer))]
    public void DiceService_Roll_For_Starting_Player_ShouldDetermineReroll(List<int> rolls, bool expectedReroll)
    {
        int highestRoll = rolls.Max();
        bool reRoll = rolls.Count(r => r == highestRoll) > 1;

        reRoll.Should().Be(expectedReroll);
    }

    public static IEnumerable<object[]> GetRollForStartingPlayer()
    {
        return new List<object[]>
        {
            // One player rolls the highest - no reroll
            new object[] { new List<int> { 4, 2, 3, 6 }, false },
            
            // Two players roll the same, but not the highest - no reroll
            new object[] { new List<int> { 4, 4, 2, 6 }, false },
            
            // Two players roll the highest - should reroll
            new object[] { new List<int> { 5, 5, 2, 3 }, true }
        };
    }
}
