namespace LudoModels;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? LatestRoll { get; set; }
}

