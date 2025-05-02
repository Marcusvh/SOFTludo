using LudoModels;
using LudoModels.Requests;
using LudoModels.Responses;
using Microsoft.AspNetCore.Mvc;
using SoftLudoApi.Services;
using SoftLudoApi.Utils;

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
    public ActionResult<IEnumerable<PlayerResponseDto>> GetPlayers()
    {
        var players = playerService.GetPlayers();

        return Mapper.ToResponseDto(players).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<PlayerResponseDto> GetPlayer(int id)
    {
        var playerResult = playerService.GetPlayer(id);

        if (playerResult.Failure) {
            return NotFound();
        }

        return Mapper.ToResponseDto(playerResult.Value!);
    }

    [HttpPost]
    public ActionResult<PlayerResponseDto> CreatePlayer([FromBody] CreatePlayerRequest createPlayerRequest)
    {
        var playerToCreate = Mapper.ToPlayer(createPlayerRequest);

        var playerResult = playerService.CreatePlayer(playerToCreate);

        if (playerResult.Failure)
        {
            return BadRequest(playerResult.ErrorType);
        }

        return Mapper.ToResponseDto(playerResult.Value!);
    }

    [HttpDelete("{id}")]
    public ActionResult DeletePlayer(int id)
    {
        var result = playerService.DeletePlayer(id);

        if (result.Success)
        {
            return NoContent();
        }

        switch (result.ErrorType)
        {
            case ErrorType.PlayerNotFound:
                return NotFound();
            default:
                return BadRequest(result.ErrorType);
        }
    }


}
