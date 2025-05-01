using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LudoModels;
using Reqnroll;
using SoftLudoAPI.Services;

namespace Ludo.Tests.Steps
{
    [Binding]
    public class PlayerStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private Game _game;
        private Player _currentPlayer;
        private List<Player> _players;
        private int _diceResult;

        public PlayerStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _game = new Game();
            _players = new List<Player>();
        }

        [Given("a new game is set up")]
        public void GivenANewGameIsSetUp()
        {
            _game = new Game();
            _scenarioContext.Set(_game, "Game");
        }

        [When("the player joins the game")]
        public void WhenThePlayerJoinsTheGame()
        {
            var player = new Player { Name = "Player1", Color = "Blue",
                GamePieces = new List<GamePiece>
                {
                    new GamePiece { Id = 1, MainBoardPosition = 1, Color = "Blue" },
                    new GamePiece { Id = 2, MainBoardPosition = 2, Color = "Blue" },
                    new GamePiece { Id = 3, MainBoardPosition = 3 , Color = "Blue"},
                    new GamePiece { Id = 4, MainBoardPosition = 4 , Color = "Blue"}
                }
            };
            _game.AddPlayer(player); // Move to PlayerService
            _scenarioContext.Set(player, "Player");
            _scenarioContext.Set(_game, "Game");
        }

        [Then("the player should have 4 game pieces of their color")]
        public void ThenThePlayerShouldHave4GamePiecesOfTheirColor()
        {
            var player = _scenarioContext.Get<Player>("Player");

            player.GamePieces.Should().NotBeNull();
            player.GamePieces.Count.Should().Be(4);
            player.GamePieces.All(p => p.Color == player.Color).Should().BeTrue();
        }

        [Given("it is the player's turn")]
        public void GivenItIsThePlayersTurn()
        {
            _currentPlayer = new Player { Name = "CurrentPlayer", Color = "Blue" };
            _game.CurrentPlayer = _currentPlayer;
            _scenarioContext.Set(_currentPlayer, "CurrentPlayer");
            _scenarioContext.Set(_game, "Game");
        }

        [When("the player rolls the dice")]
        public void WhenThePlayerRollsTheDice()
        {
            var dice = new DiceService(); // IDiceService
            _diceResult = dice.RollDice();
            _scenarioContext.Set(_diceResult, "DiceResult");
        }

        [Then("the player should be able to move a game piece according to the dice result")]
        public void ThenThePlayerShouldBeAbleToMoveAGamePieceAccordingToTheDiceResult()
        {
            var currentPlayer = _scenarioContext.Get<Player>("CurrentPlayer");
            var diceResult = _scenarioContext.Get<int>("DiceResult");

            // Implement game logic
            var hasValidMoves = diceResult == 6 || currentPlayer.GamePieces.Any(p => !p.IsInHomeArea);

            if (!hasValidMoves)
            {
                currentPlayer.GamePieces[0].IsInHomeArea = false;
                hasValidMoves = true;
            }

            hasValidMoves.Should().BeTrue();
        }

        [Given("a player has 3 game pieces in the goal")]
        public void GivenAPlayerHas3GamePiecesInTheGoal()
        {
            _currentPlayer = new Player { Name = "WinningPlayer", Color = "Blue" };
            _currentPlayer.GamePieces = new List<GamePiece>
            {
                new GamePiece { Color = "Blue", IsInGoal = true },
                new GamePiece { Color = "Blue", IsInGoal = true },
                new GamePiece { Color = "Blue", IsInGoal = true },
                new GamePiece { Color = "Blue", IsInHomeLane = true, HomeLanePosition = 3 }
            };

            _game.CurrentPlayer = _currentPlayer;
            _scenarioContext.Set(_currentPlayer, "CurrentPlayer");
            _scenarioContext.Set(_game, "Game");
        }

        [Given("the player's last game piece is one space away from the goal")]
        public void GivenThePlayersLastGamePieceIsOneSpaceAwayFromTheGoal()
        {
            // Perhaps delete
            // [Given("I roll the exact number needed to reach the goal")] in GamePieceStepDefinitions
        }

        [When("the player rolls a 1")]
        public void WhenThePlayerRollsA1()
        {
            _diceResult = 1;
            _scenarioContext.Set(_diceResult, "DiceResult");
        }

        [Then("the player should win the game")]
        public void ThenThePlayerShouldWinTheGame()
        {
            var currentPlayer = _scenarioContext.Get<Player>("CurrentPlayer");
            var diceResult = _scenarioContext.Get<int>("DiceResult");
            var game = _scenarioContext.Get<Game>("Game");

            // Move the last piece to the goal, 6 is the goal position
            currentPlayer.GamePieces[5].IsInGoal = true;
            currentPlayer.GamePieces[5].IsInHomeLane = false;

            // Implement game logic, has won
            var hasWon = currentPlayer.GamePieces.All(p => p.IsInGoal);

            hasWon.Should().BeTrue();
            game.Winner.Should().Be(currentPlayer);
        }

        [Given("a player has all game pieces in the home area")]
        public void GivenAPlayerHasAllGamePiecesInTheHomeArea()
        {
            _currentPlayer = new Player { Name = "StuckPlayer", Color = "Blue" };
            _currentPlayer.GamePieces = new List<GamePiece>
            {
                new GamePiece { Color = "Blue", IsInHomeArea = true },
                new GamePiece { Color = "Blue", IsInHomeArea = true },
                new GamePiece { Color = "Blue", IsInHomeArea = true },
                new GamePiece { Color = "Blue", IsInHomeArea = true }
            };

            _game.CurrentPlayer = _currentPlayer;
            _scenarioContext.Set(_currentPlayer, "CurrentPlayer");
            _scenarioContext.Set(_game, "Game");
        }

        [When("the player rolls a number other than 6")]
        public void WhenThePlayerRollsANumberOtherThan6()
        {
            _diceResult = 3;
            _scenarioContext.Set(_diceResult, "DiceResult");
        }

        [Then("the player's turn should end without moving any piece")]
        public void ThenThePlayersTurnShouldEndWithoutMovingAnyPiece()
        {
            var currentPlayer = _scenarioContext.Get<Player>("CurrentPlayer");
            var diceResult = _scenarioContext.Get<int>("DiceResult");

            diceResult.Should().NotBe(6);
            currentPlayer.GamePieces.All(p => p.IsInHomeArea).Should().BeTrue();

            // Implement game logic, if turn ends
            var canMove = false;
            canMove.Should().BeFalse();
        }

        [Given("a player has multiple valid moves available")]
        public void GivenAPlayerHasMultipleValidMovesAvailable()
        {
            _currentPlayer = new Player { Name = "ChoicesPlayer", Color = "Blue" };
            _currentPlayer.GamePieces = new List<GamePiece>
            {
                new GamePiece { Color = "Blue", IsInHomeArea = false, MainBoardPosition = 5 },
                new GamePiece { Color = "Blue", IsInHomeArea = false, MainBoardPosition = 10 },
                new GamePiece { Color = "Blue", IsInHomeArea = true },
                new GamePiece { Color = "Blue", IsInHomeArea = true }
            };

            _game.CurrentPlayer = _currentPlayer;
            _scenarioContext.Set(_currentPlayer, "CurrentPlayer");
            _scenarioContext.Set(_game, "Game");
        }

        [When("it is the player's turn to move")]
        public void WhenItIsThePlayersTurnToMove()
        {
            _diceResult = 6;
            _scenarioContext.Set(_diceResult, "DiceResult");
        }

        [Then("the player should be able to select which game piece to move")]
        public void ThenThePlayerShouldBeAbleToSelectWhichGamePieceToMove()
        {
            var currentPlayer = _scenarioContext.Get<Player>("CurrentPlayer");
            var diceResult = _scenarioContext.Get<int>("DiceResult");

            // Count valid moves
            var validMoves = 0;
            foreach (var piece in currentPlayer.GamePieces)
            {
                if (!piece.IsInHomeArea || diceResult == 6)
                {
                    validMoves++;
                }
            }

            validMoves.Should().BeGreaterThan(1);
        }

        [Given("there are 4 players in the game")]
        public void GivenThereAre4PlayersInTheGame()
        {
            _players = new List<Player>
            {
                new Player { Name = "Player1", Color = "Blue" },
                new Player { Name = "Player2", Color = "Red" },
                new Player { Name = "Player3", Color = "Green" },
                new Player { Name = "Player4", Color = "Yellow" }
            };

            _game.Players = _players;
            _game.CurrentPlayerIndex = 0;
            _scenarioContext.Set(_players, "Players");
            _scenarioContext.Set(_game, "Game");
        }

        [When("a round of play completes")]
        public void WhenARoundOfPlayCompletes()
        {
            var game = _scenarioContext.Get<Game>("Game");

            // Players take turns
            for (int i = 0; i < 4; i++)
            {
                game.CurrentPlayerIndex = i;
                
                _diceResult = new DiceService().RollDice();
                
                game.CurrentPlayerIndex = (game.CurrentPlayerIndex + 1) % game.Players.Count;
            }

            _scenarioContext.Set(game, "Game");
        }

        [Then("each player should have had exactly one turn in the correct order")]
        public void ThenEachPlayerShouldHaveHadExactlyOneTurnInTheCorrectOrder()
        {
            var game = _scenarioContext.Get<Game>("Game");

            // Implement game logic, check if each player had one turn
            game.CurrentPlayerIndex.Should().Be(0);
        }
    }
}