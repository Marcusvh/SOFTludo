

namespace LudoModels.Dtos;
public class GameResponse
{
    public int Id { get; set; }
    public List<Player> Players { get; set; }
    public int CurrentPlayerId { get; set; }
    public int LastRoll { get; set; }
    public string State { get; set; }
}
