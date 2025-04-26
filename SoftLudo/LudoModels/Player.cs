namespace LudoModels;

public class Player
{
    // public int Id { get; set; }      // ikke i brug, kun hvis der bliver oprettet en DB
    public string Name { get; set; } = "";
    public string Color { get; set; } = "";

    public bool IsInHomeArea { get; set; } = true;
    // public int DiceRollsLeft { get; set; } = 1;
    // public int PiecesInGoal { get; set; } = 0;

    public List<GamePiece> GamePieces { get; set; } = new List<GamePiece>();

    //public bool AnyPieceInPlay()
    //{
    //    return false;
    //    //return GamePieces.Any(p => !p.IsHome && !p.IsGoal);
    //}
}
