namespace LudoModels;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? LatestRoll { get; set; }
    public int? CurrentGameId { get; set; }
    public IEnumerable<Piece> Pieces { get; set; }

    public Player()
    {
        Pieces = new Piece[]
        {
            new Piece(),
            new Piece(),
            new Piece(),
            new Piece(),
        };
    }

}

