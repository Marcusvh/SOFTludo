using LudoModels;

namespace SoftLudoAPI.Services
{
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

        public bool CheckForSafeZone(GamePiece gp, int position)
        {
            return true;
        }

        public bool CheckForOwnGamePieceBlock(GamePiece gp, int position)
        {
            return true;
        }
    }
}
