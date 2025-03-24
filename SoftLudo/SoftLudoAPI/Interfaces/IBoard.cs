namespace SoftLudoAPI.Interfaces
{
    public interface IBoard
    {
        void InitializeBoard(int numberOfPlayers);
        int GetStartPosition(int playerId);
        bool IsGoalPosition(int position, int playerId);
        bool IsStandardTile(int position);
        bool IsPlayerPathTile(int position, int playerId);
    }
}
