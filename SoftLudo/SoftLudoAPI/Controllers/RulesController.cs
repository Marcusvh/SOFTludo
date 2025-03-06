using LudoModels;
using Microsoft.AspNetCore.Mvc;

namespace SoftLudoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        // GET: api/v1/Rules
        [HttpGet]
        public bool ValidateMove(GamePiece gp, int newPosition, List<Player> allPlayers)
        {
            return true;
        }

        [HttpGet]
        public int CheckForOtherPlayersGamePiece(int position, List<Player> allPlayers)
        {
            return 0;
        }

        [HttpGet]
        public bool CheckForWin(Player p)
        {
            return true;
        }

        [HttpGet]
        public bool CheckForSafeZone(int position)
        {
            return true;
        }

        [HttpGet]
        public bool CheckForOwnGamePieceBlock(GamePiece gp, int position)
        {
            return true;
        }
    }
}
