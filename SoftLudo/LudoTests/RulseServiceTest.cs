using LudoModels;
using SoftLudoAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoTests
{
    [TestClass]
    public class RulseServiceTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Player p1 = new()
            {
                Id = 1,
                Name = "Player 1",
                Colour = "Red",
                GamePieces = new List<GamePiece>
                {
                    new GamePiece { Id = 1, Position = 0 },
                    new GamePiece { Id = 2, Position = 0 },
                    new GamePiece { Id = 3, Position = 0 },
                    new GamePiece { Id = 4, Position = 0 }
                },
            };
            Player p2 = new()
            {
                Id = 2,
                Name = "Player 2",
                Colour = "Blue",
                GamePieces = new List<GamePiece>
                {
                    new GamePiece { Id = 5, Position = 0 },
                    new GamePiece { Id = 6, Position = 0 },
                    new GamePiece { Id = 7, Position = 0 },
                    new GamePiece { Id = 8, Position = 0 }
                },
            };
            Player p3 = new()
            {
                Id = 3,
                Name = "Player 3",
                Colour = "Green",
                GamePieces = new List<GamePiece>
                {
                    new GamePiece { Id = 9, Position = 0 },
                    new GamePiece { Id = 10, Position = 0 },
                    new GamePiece { Id = 11, Position = 0 },
                    new GamePiece { Id = 12, Position = 0 }
                },
            };

            Player p4 = new()
            {
                Id = 4,
                Name = "Player 4",
                Colour = "Yellow",
                GamePieces = new List<GamePiece>
                {
                    new GamePiece { Id = 13, Position = 0 },
                    new GamePiece { Id = 14, Position = 0 },
                    new GamePiece { Id = 15, Position = 0 },
                    new GamePiece { Id = 16, Position = 0 }
                },
            };

            List<Player> allPlayers = new() { p1, p2, p3, p4 };

            RulesService rs = new(allPlayers);
        }

        [TestMethod]
        public bool RulseService_ValidateMove_ExpectIntReturn()
        {
            return false;
        }
        [TestMethod]
        public int RulseService_CheckForOtherPlayersGamePiece_ExpectIntReturn()
        {
            return 0;
        }
        [TestMethod]
        public bool RulseService_CheckForWin_ExpectIntReturn()
        {
            return false;
        }
        [TestMethod]
        public int RulseService_RemainingPlayers_ExpectIntReturn()
        {
            return 0;
        }
        [TestMethod]
        public bool RulseService_CheckForSafeZone_ExpectIntReturn()
        {
            return false;
        }

        [TestMethod]
        public bool RulseService_CheckForOwnGamePieceBlock_ExpectIntReturn()
        {
            return false;
        }

        [TestMethod]
        public int RulseService_CheckForRemainingStepsTowardsGoal_ExpectIntReturn()
        {
            return 0;
        }
    }
}
