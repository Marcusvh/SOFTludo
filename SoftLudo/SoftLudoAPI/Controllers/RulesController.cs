using LudoModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
    }
}
