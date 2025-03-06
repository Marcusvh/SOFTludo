using SoftLudoAPI.Services;

namespace LudoTests
{
    [TestClass]
    public class Setup
    {
        [TestInitialize]
        public void TestInitialize()
        {
            SetupService setup = new();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(5)]
        public void SetupServicePlayerCountExpectFail(int playerCount)
        {
            SetupService setup = new();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                setup.PlayerCount = playerCount;
            });
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void SetupServicePlayerCountExpectSuccess(int playerCount)
        {
            SetupService setup = new();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                setup.PlayerCount = playerCount;
            });
        }
    }
}
