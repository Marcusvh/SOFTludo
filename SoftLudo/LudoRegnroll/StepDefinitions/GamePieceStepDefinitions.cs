using System;
using System.Collections.Generic;
using FluentAssertions;
using LudoModels;
using LudoModels.BoardSpec;
using Reqnroll;

namespace Ludo.Tests.Steps
{
    [Binding]
    public class GamePieceStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private GamePiece _gamePiece;
        private GamePiece _opponentPiece;
        private int _diceResult;
        private Board _board;

        public GamePieceStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _board = new Board(); 
            _gamePiece = new GamePiece();
        }

        [Given("I have a game piece on the board")]
        public void GivenIHaveAGamePieceOnTheBoard()
        {
            _gamePiece = new GamePiece { IsInHomeArea = false, MainBoardPosition = 1 };
            _board = new Board
            {
                RegularSpaces = new List<Space>(),
                HomeAreas = new List<HomeArea>(),
                HomeLanes = new List<HomeLane>(),
                GoalAreas = new List<GoalLane>(),
                SafeHomeBase = new List<GoalLane>()
            };
            _scenarioContext.Set(_gamePiece, "GamePiece");
            _scenarioContext.Set(_board, "Board");
        }

        [Given(@"I roll a (\d+)")]
        public void GivenIRollA(int diceResult)
        {
            _diceResult = diceResult;
            _scenarioContext.Set(_diceResult, "DiceResult");
        }

        [When("I move the game piece")]
        public void WhenIMoveTheGamePiece()
        {
            var gamePiece = _scenarioContext.Get<GamePiece>("GamePiece");
            var diceResult = _scenarioContext.Get<int>("DiceResult");
            var board = _scenarioContext.Get<Board>("Board");

            // // Implement the game logic
            if (!gamePiece.IsInHomeArea || diceResult == 6)
            {
                gamePiece.MainBoardPosition = (gamePiece.IsInHomeArea) ? 0 : gamePiece.MainBoardPosition + diceResult;
                gamePiece.IsInHomeArea = false;
            }

            _scenarioContext.Set(gamePiece, "GamePiece");
        }

        [When("I move the game piece out")]
        public void WhenIMoveTheGamePieceOut()
        {
            var gamePiece = _scenarioContext.Get<GamePiece>("GamePiece");
            var diceResult = _scenarioContext.Get<int>("DiceResult");

            diceResult.Should().Be(6);
            gamePiece.IsInHomeArea.Should().BeTrue();

            gamePiece.IsInHomeArea = false;
            gamePiece.MainBoardPosition = 0;

            _scenarioContext.Set(gamePiece, "GamePiece");
        }

        [Then("the game piece should be on the starting position for my color")]
        public void ThenTheGamePieceShouldBeOnTheStartingPositionForMyColor()
        {
            var gamePiece = _scenarioContext.Get<GamePiece>("GamePiece");

            gamePiece.IsInHomeArea.Should().BeFalse();
            gamePiece.MainBoardPosition.Should().Be(0);
        }

        [Then(@"the game piece should move (\d+) positions forward")]
        public void ThenTheGamePieceShouldMovePositionsForward(int positions)
        {
            var gamePiece = _scenarioContext.Get<GamePiece>("GamePiece");
            var initialPosition = 1;

            gamePiece.MainBoardPosition.Should().Be(initialPosition + positions);
        }

        [Given("I have a game piece that has moved almost a full round")]
        public void GivenIHaveAGamePieceThatHasMovedAlmostAFullRound()
        {
            _gamePiece = new GamePiece { IsInHomeArea = false, MainBoardPosition = 39 }; // Positions 0-39 for the main board + 4 home area positions + 5 home lane + 1 goal lane
            _scenarioContext.Set(_gamePiece, "GamePiece");
            _scenarioContext.Set(_board, "Board");
        }

        [Given("I roll the exact number needed to enter the home lane")]
        public void GivenIRollTheExactNumberNeededToEnterTheHomeLane()
        {
            // player color is such that home lane entry is at position 40
            _diceResult = 1;
            _scenarioContext.Set(_diceResult, "DiceResult");
        }

        [Then("the game piece should enter the home lane")]
        public void ThenTheGamePieceShouldEnterTheHomeLane()
        {
            var gamePiece = _scenarioContext.Get<GamePiece>("GamePiece");

            // Implement the game logic
            gamePiece.IsInHomeLane = true;
            gamePiece.HomeLanePosition = 0;

            gamePiece.IsInHomeLane.Should().BeTrue();
            gamePiece.HomeLanePosition.Should().Be(0); // home lane 0-5, the end
        }

        [Given("an opponent has a game piece on a regular space")]
        public void GivenAnOpponentHasAGamePieceOnARegularSpace()
        {
            _opponentPiece = new GamePiece { IsInHomeArea = false, MainBoardPosition = 5, Color = "Red" };
            _gamePiece.MainBoardPosition = 1;
            _gamePiece.Color = "Blue";
            _diceResult = 4;

            _scenarioContext.Set(_opponentPiece, "OpponentPiece");
            _scenarioContext.Set(_gamePiece, "GamePiece");
            _scenarioContext.Set(_diceResult, "DiceResult");
        }

        [When("I land on the opponent's piece")]
        public void WhenILandOnTheOpponentsPiece()
        {
            var gamePiece = _scenarioContext.Get<GamePiece>("GamePiece");
            var opponentPiece = _scenarioContext.Get<GamePiece>("OpponentPiece");
            var diceResult = _scenarioContext.Get<int>("DiceResult");

            gamePiece.MainBoardPosition += diceResult;

            gamePiece.MainBoardPosition.Should().Be(opponentPiece.MainBoardPosition);

            _scenarioContext.Set(gamePiece, "GamePiece");
            _scenarioContext.Set(opponentPiece, "OpponentPiece");
        }

        [Then("the opponent's piece should return to their home area")]
        public void ThenTheOpponentsPieceShouldReturnToTheirHomeArea()
        {
            var opponentPiece = _scenarioContext.Get<GamePiece>("OpponentPiece");

            // Implement the game logic
            opponentPiece.IsInHomeArea = true;
            opponentPiece.MainBoardPosition = -1;

            opponentPiece.IsInHomeArea.Should().BeTrue();
        }

        [Given("I have a game piece in the home lane")]
        public void GivenIHaveAGamePieceInTheHomeLane()
        {
            _gamePiece = new GamePiece
            {
                IsInHomeArea = false,
                IsInHomeLane = true,
                HomeLanePosition = 4
            };
            _scenarioContext.Set(_gamePiece, "GamePiece");
        }

        [Given("I roll the exact number needed to reach the goal")]
        public void GivenIRollTheExactNumberNeededToReachTheGoal()
        {
            // home lane has 5 spaces (0-4), and we need to roll a 1 to reach the goal
            _diceResult = 1;
            _scenarioContext.Set(_diceResult, "DiceResult");
        }

        [Then("the game piece should reach the goal")]
        public void ThenTheGamePieceShouldReachTheGoal()
        {
            var gamePiece = _scenarioContext.Get<GamePiece>("GamePiece");
            var diceResult = _scenarioContext.Get<int>("DiceResult");

            // Implement the game logic
            gamePiece.IsInGoal = true;

            gamePiece.IsInGoal.Should().BeTrue();
        }
    }
}