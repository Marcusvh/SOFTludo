using SoftLudoAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoTests
{
    [TestClass]
    public class GamePieceServiceTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            GamePieceService gps = new();
        }

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
}
