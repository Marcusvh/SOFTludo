namespace LudoModels;

public class Player
{
    public string Name { get; set; }
    public int Id { get; set; }
    public List<GamePiece> Tokens { get; set; } = new List<GamePiece>();
    public bool HasWon { get; set; } = false;
}
}
