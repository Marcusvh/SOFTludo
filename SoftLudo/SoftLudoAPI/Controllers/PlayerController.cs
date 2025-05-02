using Microsoft.AspNetCore.Mvc;
using SoftLudoApi.Services;

namespace SoftLudoApi.Controllers;

[Route("[controller]s")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService playerService;
    public PlayerController(IPlayerService playerService) 
    { 
        this.playerService = playerService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Player>> Get()
    {
        var players = playerService.GetPlayers();

        return Ok(players);
    }
}
