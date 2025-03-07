using LudoModels;
using SoftLudoAPI.Services;

namespace LudoTests
{
    [TestClass]
    public class DiceServiceTest
    {
        DiceService diceService = new();


        [TestMethod]
        [DataRow(null)]
        public void DiceService_RollDice_ExpectFail(Player player)
        {   
            Assert.ThrowsException<ArgumentException>(() => diceService.RollDice(player));
        }
        [TestMethod]
        public void DiceService_RollDice_ExpectSuccess()
        {
            Player player = new();

            int result = diceService.RollDice(player);

            Assert.IsTrue(result >= 1 && result <= 6);
        }
        [TestMethod]
        public void DiceService_IfRollSix_ExpectSuccess() // TODO: 
        {
            Player player = new();

            diceService.IfRollSix(player);

            Assert.Fail();
        }
    }
}
