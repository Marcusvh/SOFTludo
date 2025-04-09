namespace LudoModels.Requests;
public class PlayTurnRequest
{
    public int PlayerId { get; set; }

    public Command Command { get; set; } = null!;

}
