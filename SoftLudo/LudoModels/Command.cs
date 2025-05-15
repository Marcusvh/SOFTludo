namespace LudoModels;
public class Command
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int GameId { get; set; }
    public CommandType Type { get; set; }
    public int? PieceId { get; set; }
    public int BeforeCommandFieldId { get; set; }
    public int AfterCommandFieldId { get; set; }

}
