namespace LudoModels.Requests;
public class PlayTurnRequest
{
    public int PlayerId { get; set; }
    public Command Command { get; set; } = Command.InvalidMove; // Default to a valid enum value  
    public int PieceId { get; set; }
}
