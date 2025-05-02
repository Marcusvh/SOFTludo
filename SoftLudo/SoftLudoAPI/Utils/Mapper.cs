using LudoModels;
using LudoModels.Dtos;

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
}
