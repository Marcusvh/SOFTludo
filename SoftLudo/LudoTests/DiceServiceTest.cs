using SoftLudoAPI.Services;

namespace LudoTests
{
    [TestClass]
    public class DiceServiceTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            DiceService diceService = new();
        }


        [TestMethod]
        public int DiceService_RollDice_ExpectIntReturn()
        {

            DiceService diceService = new();

            return 0;
        }
    }
}
