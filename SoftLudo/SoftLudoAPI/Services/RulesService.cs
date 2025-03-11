using LudoModels;

namespace SoftLudoAPI.Services;

public class RulesService(List<Player> allPlayers)
{
    public List<Player> allPlayers = allPlayers;
    public bool ValidateMove(GamePiece gp, int position)
    {
        return true;
    }

    public int CheckForOtherPlayersGamePiece(int position)
    {
        return 0;
    }

    public bool CheckForWin(Player p)
    {
        return true;
    }
    public int RemainingPlayers() 
    { 
        return 0; 
    }
    public Player NextPlayer()
    {
        return new Player();
    }
    public bool CheckForSafeZone(int position)
    {
        return true;
    }

    public bool CheckForOwnGamePieceBlock(GamePiece gp, int position)
    {
        return true;
    }
    public int CheckForRemainingStepsTowardsGoal(GamePiece gp)
    {
        return 0;
    }
}
