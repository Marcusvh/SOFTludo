namespace LudoModels;
public class GamePiece
{
    public int Position { get; set; }
    public bool IsHome { get; set; } = true;
    public bool IsInGoal { get; set; } = false;
}
