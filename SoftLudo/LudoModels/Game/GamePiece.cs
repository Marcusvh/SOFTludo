namespace LudoModels.Game;
public class GamePiece
{
    public int Id { get; set; }
    //public int Position { get; set; } // maybe have it as a string, so we can write A2, B3, etc. or 04-51 for the board. XY axis based.
    //public bool IsHome { get; set; } = true;
    //public bool IsGoal { get; set; } = false;
    //public bool IsSafe { get; set; } = true;

    public string Color { get; set; }
    public bool IsInHomeArea { get; set; } = true;
    public bool IsInHomeLane { get; set; } = false;
    public bool IsInGoal { get; set; } = false;
    public bool IsSafeHomeBase { get; set; } = true;
    public int MainBoardPosition { get; set; } = -1; // -1 indicates not on main board
    public int HomeAreaPosition { get; set; } = -1; // Position in home area (0-4)
    public int HomeLanePosition { get; set; } = -1; // Position in home lane (0-5)
    
    // public int SafeHomeBase { get; set; } = 5; // Position 5.....
}
