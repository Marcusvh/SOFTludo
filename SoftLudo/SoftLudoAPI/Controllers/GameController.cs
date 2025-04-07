using Microsoft.AspNetCore.Mvc;
using SoftLudoAPI.Services;

namespace SoftLudoAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService gameService;

    public GameController(IGameService gameService)
    {
        this.gameService = gameService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
