namespace LudoModels;

public interface IDice
{
    int Roll();
    int RollsRemaining { get; set; }
}
