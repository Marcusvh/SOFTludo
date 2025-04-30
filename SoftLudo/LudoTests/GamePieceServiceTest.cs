using LudoModels;
using SoftLudoAPI.Services;

namespace LudoTests;

[TestClass]
public class GamePieceServiceTest
{
    GamePieceService gps = new();
    GamePiece gp = new();

    // Models
    //////////////////////////////////////////
    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    public void GamePiece_Id_ExpectFail(int id)
    {
        gp.Id = id;

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => gps.MoveGamePiece(gp, 0));
    }
    [TestMethod]
    [DataRow(1)]
    [DataRow(int.MaxValue)]
    public void GamePiece_Id_ExpectSuccess(int id)
    {
        gp.Id = id;

        Assert.AreEqual(id, gp.Id);
    }

    // TODO: Position
    // TODO: when Position type is decided, add tests for when the bools are correct. such as when position is X, is that home/goal/safe?

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void GamePiece_IsHome_ExpectSuccess(bool isHome)
    {
        gp.IsInHomeArea = isHome;
        Assert.AreEqual(isHome, gp.IsInHomeArea);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void GamePiece_IsGoal_ExpectSuccess(bool isGoal)
    {
        gp.IsInGoal = isGoal;
        Assert.AreEqual(isGoal, gp.IsInGoal);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void GamePiece_IsSafe_ExpectSuccess(bool isSafe)
    {
        gp.IsSafeHomeBase = isSafe;
        Assert.AreEqual(isSafe, gp.IsSafeHomeBase);
    }

    // Services
    //////////////////////////////////////////
    [TestMethod]
    public bool GamePieceService_MoveGamePiece_ExpectIntReturn()
    {
        return false;
    }

    [TestMethod]
    public int GamePieceService_MoveGamePieceToGoal_ExpectIntReturn()
    {
        return 0;
    }

    [TestMethod]
    public bool GamePieceService_MoveGamePieceToHome_ExpectIntReturn()
    {
        return false;
    }

    [TestMethod]
    public int GamePieceService_MoveGamePieceFromHomeToStart_ExpectIntReturn()
    {
        return 0;
    }

    [TestMethod]
    public bool GamePieceService_MoveGamePieceToGoalTrack_ExpectIntReturn()
    {
        return false;
    }

}
