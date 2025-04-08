using LudoModels;
using LudoModels.Dtos;
using LudoModels.Requests;
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


    [HttpGet("")]
    public ActionResult<IEnumerable<GameResponse>> GetGames()
    {
        var games = gameService.GetGames();

        //mapping here?
        return Ok(games);
    }


    [HttpGet("{id}")]
    public ActionResult<GameResponse> GetGame(int id)
    {
        var game = gameService.GetGame(id);

        if (game == null)
        {
            return NoContent();
        }
        //mapping here?
        return Ok(game);
    }

    [HttpPost]
    public ActionResult<GameResponse> CreateGame([FromBody] int playerId)
    {
        var game = gameService.CreateGame(playerId);

        if (game == null)
        {
            return BadRequest("Burde vi bruge en result patterne ller kaste exceptions for korrekte fejlbeskeder?");
        }    

        return Ok(game);
    }

#warning kan vi forbedre routes?
    [HttpPost("{id}/join")]
    public ActionResult<GameResponse> JoinGame(int id, [FromBody] int playerId)
    {
        var game = gameService.JoinGame(playerId, id);

        if (game == null)
        {
            return BadRequest("Burde vi bruge en result patterne ller kaste exceptions for korrekte fejlbeskeder?");
        }

        return Ok(game);
    }

#warning kan vi forbedre routes?
    [HttpPost("{id}/start")]
    public ActionResult<GameResponse> StartGame(int id, [FromBody] int playerId)
    {
        var game = gameService.StartGame(playerId, id);

        if (game == null)
        {
            return BadRequest("Burde vi bruge en result patterne ller kaste exceptions for korrekte fejlbeskeder?");
        }

        return Ok(game);
    }

#warning kan vi forbedre routes?
    [HttpPost("{id}/roll")]
    public ActionResult<GameResponse> Roll(int id, [FromBody] int playerId)
    {
        var game = gameService.Roll(playerId, id);

        if (game == null)
        {
            return BadRequest("Burde vi bruge en result patterne ller kaste exceptions for korrekte fejlbeskeder?");
        }

        return Ok(game);
    }

#warning kan vi forbedre routes?
    [HttpPost("{id}/play")]
    public ActionResult<GameResponse> PlayTurn(int id, [FromBody] PlayTurnRequest request)
    {
#warning baser command på PlayTurnRequest fra body
        var command = new Command(); //based on PlayTurnRequest request

        var game = gameService.PlayTurn(request.PlayerId, id, command);

        if (game == null)
        {
            return BadRequest("Burde vi bruge en result patterne ller kaste exceptions for korrekte fejlbeskeder?");
        }

        return Ok(game);
    }


}

