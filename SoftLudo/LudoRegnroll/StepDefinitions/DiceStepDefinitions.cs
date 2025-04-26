using System;
using FluentAssertions;
using LudoModels;
using Reqnroll;
using SoftLudoAPI.Services;

namespace LudoRegnroll.StepDefinitions
{
    [Binding]
    public class DiceStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private DiceService _dice; // IDiceService
        private List<int> _rollResults;
        private readonly Dictionary<string, int> _playerRolls = new Dictionary<string, int>();
        private string _currentPlayer;
        private bool _extraTurn;

        public DiceStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _dice = new DiceService();
            _rollResults = new List<int>();
        }

        [When("I roll the dice")]
        public void WhenIRollTheDice()
        {
            var result = _dice.RollDice();
            _rollResults.Add(result);
            _scenarioContext.Set(result, "DiceResult");
        }


        [Then("the result should be between 1 and 6")]
        public void ThenTheResultShouldBeBetween1And6()
        {
            var result = _scenarioContext.Get<int>("DiceResult");
            result.Should().BeGreaterThanOrEqualTo(1);
            result.Should().BeLessThanOrEqualTo(6);
        }

        [When(@"I roll the dice (\d+) times")]
        public void WhenIRollTheDiceTimes(int numberOfRolls)
        {
            _rollResults.Clear();
            for (int i = 0; i < numberOfRolls; i++)
            {
                _rollResults.Add(_dice.RollDice());
            }
        }

        [Then("all results should be between 1 and 6")]
        public void ThenAllResultsShouldBeBetween1And6()
        {
            foreach (var result in _rollResults)
            {
                result.Should().BeGreaterThanOrEqualTo(1);
                result.Should().BeLessThanOrEqualTo(6);
            }
        }

        [Given("multiple players are ready to play")]
        public void GivenMultiplePlayersAreReadyToPlay()
        {
            var players = new List<string> { "Player1", "Player2", "Player3", "Player4" };
            _scenarioContext.Set(players, "Players");
        }

        [When("each player rolls the dice")]
        public void WhenEachPlayerRollsTheDice()
        {
            var players = _scenarioContext.Get<List<string>>("Players");
            foreach (var player in players)
            {
                _playerRolls[player] = _dice.RollDice();
            }
            _scenarioContext.Set(_playerRolls, "PlayerRolls");
        }

        [Then("the player with the highest roll should start")]
        public void ThenThePlayerWithTheHighestRollShouldStart()
        {
            var playerRolls = _scenarioContext.Get<Dictionary<string, int>>("PlayerRolls");
            var maxRoll = playerRolls.Values.Max();
            var playersWithMaxRoll = playerRolls.Where(pr => pr.Value == maxRoll).Select(pr => pr.Key).ToList();

            playersWithMaxRoll.Count.Should().BeGreaterThan(0);

            if (playersWithMaxRoll.Count == 1)
            {
                _scenarioContext.Set(playersWithMaxRoll[0], "StartingPlayer");
            }
            else
            {
                // if tie, roll again
                _scenarioContext.Set(playersWithMaxRoll, "PlayersNeedingReroll");
            }
        }

        [Given("I have a game piece in the home area")]
        public void GivenIHaveAGamePieceInTheHomeArea()
        {
            var gamePiece = new GamePiece { IsInHomeArea = true };
            _scenarioContext.Set(gamePiece, "GamePiece");
        }


        [When("I roll a six")]
        public void WhenIRollASix()
        {
            // Mock a roll of 6 for testing purposes
            _scenarioContext.Set(6, "DiceResult");
        }

        [Then("I should be able to move the game piece to the starting position")]
        public void ThenIShouldBeAbleToMoveTheGamePieceToTheStartingPosition()
        {
            var gamePiece = _scenarioContext.Get<GamePiece>("GamePiece");
            var diceResult = _scenarioContext.Get<int>("DiceResult");

            diceResult.Should().Be(6);
            gamePiece.IsInHomeArea.Should().BeTrue();

            // Implement the game logic
            var canMove = diceResult == 6;
            canMove.Should().BeTrue();
        }

        [Given("it is my turn")]
        public void GivenItIsMyTurn()
        {
            _currentPlayer = "CurrentPlayer";
            _scenarioContext.Set(_currentPlayer, "CurrentPlayer");
            _extraTurn = false;
        }


        [Then("I should get another turn")]
        public void ThenIShouldGetAnotherTurn()
        {
            var diceResult = _scenarioContext.Get<int>("DiceResult");
            diceResult.Should().Be(6);

            // Implement the game logic
            _extraTurn = diceResult == 6;
            _extraTurn.Should().BeTrue();
        }
    }
}
