namespace LudoModels;
public class Command
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int GameId { get; set; }
    public CommandType Type { get; set; }
    public int? TargetId { get; set; }
}
