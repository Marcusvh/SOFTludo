using LudoModels;
using LudoModels.Dtos;
using LudoModels.Requests;
using Microsoft.AspNetCore.Mvc;
using SoftLudoAPI.Services;

namespace SoftLudoAPI.Controllers;

[Route("[controller]s")]
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

        return Ok(games);
    }


    [HttpGet("{id}")]
    public ActionResult<GameResponse> GetGame(int id)
    {
        var game = gameService.GetGame(id);

        if (!game.Success)
        {
            return NoContent();
        }

        return Ok(game);
    }

    [HttpPost]
    public ActionResult<GameResponse> CreateGame([FromBody] int playerId)
    {
        var game = gameService.CreateGame(playerId);

        if (game.Failure)
        {
            return BadRequest(game.ErrorType);
        }

        return Ok(game.Value);
    }

    [HttpPost("{id}/join")]
    public ActionResult<GameResponse> JoinGame(int id, [FromBody] int playerId)
    {
        var game = gameService.JoinGame(playerId, id);

        if (!game.Success)
        {
            return BadRequest(game.ErrorType);
        }

        return Ok(game.Value);
    }

    [HttpPost("{id}/start")]
    public ActionResult<GameResponse> StartGame(int id, [FromBody] int playerId)
    {
        var game = gameService.StartGame(playerId, id);

        if (!game.Success)
        {
            return BadRequest(game.ErrorType);
        }

        return Ok(game.Value);
    }

    [HttpPost("{id}/roll")]
    public ActionResult<GameResponse> Roll(int id, [FromBody] int playerId)
    {
        var game = gameService.Roll(playerId, id);

        if (!game.Success)
        {
            return BadRequest(game.ErrorType);
        }

        return Ok(game.Value);
    }

    [HttpPost("{id}/play")]
    public ActionResult<GameResponse> PlayTurn(int id, [FromBody] PlayTurnRequest request)
    {
        var game = gameService.PlayTurn(request.PlayerId, id, request.Command);

        if (!game.Success)
        {
            return BadRequest(game.ErrorType);
        }

        return Ok(game.Value);
    }


}

