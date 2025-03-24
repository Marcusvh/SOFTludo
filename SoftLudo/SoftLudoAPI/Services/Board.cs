using SoftLudoAPI.Interfaces;

namespace SoftLudoAPI.Services
{
    public class Board : IBoard
    {
        public void InitializeBoard(int numberOfPlayers) { }
        public int GetStartPosition(int playerId) => 0;
        public bool IsGoalPosition(int position, int playerId) => false;
        public bool IsStandardTile(int position) => false;
        public bool IsPlayerPathTile(int position, int playerId) => false;
    }
}
