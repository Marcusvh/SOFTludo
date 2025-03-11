using SoftLudoAPI.Services;

namespace LudoTests;

[TestClass]
public class Setup
{
    SetupService setup = new();

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(5)]
    public void SetupServicePlayerCountExpectFail(int playerCount)
    {   
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
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            setup.PlayerCount = playerCount;
        });
    }
    [TestMethod]
    [DataRow("red")]
    [DataRow("green")]
    [DataRow("blue")]
    [DataRow("yellow")]
    public void SetupServiceChoseColourExpectSuccess(string colour)
    {
        Assert.AreEqual(colour, setup.ChoseColour(colour));
    }
    [TestMethod]
    [DataRow("purple")]
    [DataRow("orange")]
    [DataRow("black")]
    [DataRow("white")]
    public void SetupServiceChoseColourExpectFail(string colour)
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            setup.ChoseColour(colour);
        });
    }
    [TestMethod]
    public void SetupServiceGetStartingPlayerExpectSuccess()
    {
        SetupService setup = new();
        Assert.IsNotNull(setup.GetStartingPlayer());
    }
    [TestMethod]
    public void SetupServiceGetStartingPlayerExpectFail()
    {
        SetupService setup = new();
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            setup.GetStartingPlayer();
        });
    }
    [TestMethod]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(4)]
    public void SetupServiceSetupExpectSuccess(int playerCount)
    {
        SetupService setup = new();
        Assert.IsTrue(setup.Setup(playerCount));
    }
    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(5)]
    public void SetupServiceSetupExpectFail(int playerCount)
    {
        
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            setup.Setup(playerCount);
        });
    }

}
