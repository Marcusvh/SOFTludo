using Microsoft.AspNetCore.Mvc;

namespace SoftLudoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiceController : ControllerBase
    {
        // GET: api/v1/Dice
        [HttpGet]
        public int RollDice()
        {
            return 0;
        }

        // GET api/v1/Dice/RollsLeft
        [HttpGet]
        public int RollsLeft()
        {
            return 0;
        }
    }
}
