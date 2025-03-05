using Microsoft.AspNetCore.Mvc;
using LudoModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftLudoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        // GET: api/<SetupController>
        [HttpGet]
        public bool Get(int playerCount)
        {
            return true;
        }

        // GET api/<SetupController>/ChoseColour
        [HttpGet("ChoseColour")]
        public string ChoseColour(string colour)
        {
            return "value";
        }

        // GET api/<SetupController>/GetStartingPlayer
        [HttpGet("GetStartingPlayer")]
        public Player GetStartingPlayer()
        {
            return new Player();

        }
    }
}
