namespace LudoModels;

public class Dice : IDice
{
    private readonly int min;
    private readonly int max;
    private readonly Random random;

    public int Roll()
    {
        return random.Next(min, max);
    }

    public Dice(int min, int max, int seed = 0)
    {
        random = seed > 0 ? new Random(seed) : new Random();
        this.min = min;
        this.max = max;        
    }
}
