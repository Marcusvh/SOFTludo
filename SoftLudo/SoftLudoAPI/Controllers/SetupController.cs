using Microsoft.AspNetCore.Mvc;
using LudoModels;

namespace SoftLudoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        // GET: api/v1/Setup
        [HttpGet]
        public bool Get(int playerCount)
        {
            return true;
        }

        // GET api/v1/Setup/ChoseColour
        [HttpGet("ChoseColour")] //post?, to edit the colour of the players.
        public string ChoseColour(string colour)
        {
            return "value";
        }

        // GET api/v1/Setup/GetStartingPlayer
        [HttpGet("GetStartingPlayer")]
        public Player GetStartingPlayer()
        {
            return new Player();
        }
    }
}
