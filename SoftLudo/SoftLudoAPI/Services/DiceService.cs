using LudoModels;

namespace SoftLudoAPI.Services;

public class DiceService
{
    public int RollDice() 
    {
        Random rnd = new Random();
        var diceNum = rnd.Next(1, 7);
        return diceNum;
    }

    public int RollDice(Player p)
    {
        return 0;
    }

    public void IfRollSix(Player p) // void?
    {

    }
    public List<Player> FindPlayerOrder(int players)
    {
        return new List<Player>();
    }
}
