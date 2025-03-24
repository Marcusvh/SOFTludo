using LudoModels;
using SoftLudoAPI.Interfaces;

namespace SoftLudoAPI.Services
{
    public class RuleSet : IRuleSet
    {
        public bool CanMove(Player player, GamePiece token, int diceRoll) => false;
        public bool CheckWinCondition(Player player) => false;
        public int GetValidMovePosition(GamePiece token, int diceRoll) => 0;
        public bool IsPlayerBlocked(Player player) => false;
        public int DetermineStartingPlayer(List<Player> players, IDice dice) => 0;
    }
}
