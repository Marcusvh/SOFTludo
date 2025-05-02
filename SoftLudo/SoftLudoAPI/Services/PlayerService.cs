using LudoModels;
using SoftLudoApi.Repositories;

namespace SoftLudoApi.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository playerRepository;

    public PlayerService(IPlayerRepository playerRepository) 
    {
        this.playerRepository = playerRepository;
    }

    public Result<Player> CreatePlayer(Player player)
    {
        return playerRepository.SavePlayer(player);
    }

    public Result<bool> DeletePlayer(int id)
    {
        var didDelete = playerRepository.DeletePlayer(id);

        if (didDelete)
        {
            return new Result<bool>(didDelete);
        }

        return new Result<bool>(ErrorType.PlayerNotFound);
    }

    public Result<Player> GetPlayer(int id)
    {
        var player = playerRepository.GetPlayer(id);
        
        if (player == null)
        {
            return new Result<Player>(ErrorType.PlayerNotFound);
        }
        
        return new Result<Player>(player);
    }

    public IEnumerable<Player> GetPlayers()
    {
        return playerRepository.GetPlayers();
    }
}
