using LudoModels;
using LudoModels.Requests;
using LudoModels.Responses;

namespace SoftLudoApi.Utils;

public static class Mapper
{
    public static PlayerResponseDto ToResponseDto(Player player)
    {
        var result = new PlayerResponseDto
        {
            Id = player.Id,
            Name = player.Name,
        };
        return result;
    }

    public static IEnumerable<PlayerResponseDto> ToResponseDto(IEnumerable<Player> players)
    {
        return players.Select(ToResponseDto);
    }

    public static Player ToPlayer(CreatePlayerRequest request)
    {
        var player = new Player
        {
            Name = request.Name,
        };
        return player;
    }
}
