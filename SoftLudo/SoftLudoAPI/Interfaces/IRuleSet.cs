using LudoModels;
using System.ComponentModel;

namespace SoftLudoAPI.Interfaces
{
    public interface IRuleSet
    {
        bool CanMove(Player player, GamePiece token, int diceRoll);
        bool CheckWinCondition(Player player);
        int GetValidMovePosition(GamePiece token, int diceRoll);
        bool IsPlayerBlocked(Player player);
        int DetermineStartingPlayer(List<Player> players, IDice dice);
    }
}
