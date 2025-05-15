namespace LudoModels;

public class Dice : IDice
{
    public int RollsRemaining { get; set; }
    private readonly int min;
    private readonly int max;
    private readonly Random random;

    public int Roll()
    {
        var result = random.Next(min, max + 1);
        if (result == max)
        {
            RollsRemaining = 1;
        }

        return result;
    }

    public Dice(int min, int max, int seed = 0)
    {
        random = seed > 0 ? new Random(seed) : new Random();
        this.min = min;
        this.max = max;        
    }
}
