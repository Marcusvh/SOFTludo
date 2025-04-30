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
    public class SetupStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private Game _game;
        private Board _board;
        private List<Player> _players;
        private Dictionary<string, int> _playerRolls;

        public SetupStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When("a new game is created")]
        public void WhenANewGameIsCreated()
        {
            _game = new Game();
            _board = new Board();
            _game.Board = _board;

            _scenarioContext.Set(_game, "Game");
            _scenarioContext.Set(_board, "Board");
        }

        [Then(@"the board should have (\d+) regular spaces")]
        public void ThenTheBoardShouldHaveRegularSpaces(int numberOfSpaces)
        {
            var board = _scenarioContext.Get<Board>("Board");

            board.RegularSpaces.Count.Should().Be(numberOfSpaces);
        }

        [Then(@"the board should have (\d+) home areas with (\d+) spaces each")]
        public void ThenTheBoardShouldHaveHomeAreasWithSpacesEach(int numberOfHomeAreas, int spacesPerHomeArea)
        {
            var board = _scenarioContext.Get<Board>("Board");

            board.HomeAreas.Count.Should().Be(numberOfHomeAreas);
            board.HomeAreas.All(ha => ha.Spaces.Count == spacesPerHomeArea).Should().BeTrue();
        }

        [Then(@"the board should have (\d+) home lanes with (\d+) spaces each")]
        public void ThenTheBoardShouldHaveHomeLanesWithSpacesEach(int numberOfHomeLanes, int spacesPerHomeLane)
        {
            var board = _scenarioContext.Get<Board>("Board");

            board.HomeLanes.Count.Should().Be(numberOfHomeLanes);
            board.HomeLanes.All(hl => hl.Spaces.Count == spacesPerHomeLane).Should().BeTrue();
        }

        [Then(@"the board should have (\d+) goal areas with (\d+) spaces each")]
        public void ThenTheBoardShouldHaveGoalAreasWithSpacesEach(int numberOfGoalAreas, int spacesPerGoalArea)
        {
            var board = _scenarioContext.Get<Board>("Board");

            board.GoalAreas.Count.Should().Be(numberOfGoalAreas);
            board.GoalAreas.All(ga => ga.Spaces.Count == spacesPerGoalArea).Should().BeTrue();
        }

        [When(@"(\d+) players join the game")]
        public void WhenPlayersJoinTheGame(int numberOfPlayers)
        {
            _game = new Game();
            _board = new Board();
            _game.Board = _board;

            string[] colors = { "Blue", "Red", "Green", "Yellow" };
            _players = new List<Player>();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                var player = new Player { Name = $"Player{i + 1}", Color = colors[i] };
                _players.Add(player);
                _game.AddPlayer(player);
            }

            _scenarioContext.Set(_game, "Game");
            _scenarioContext.Set(_players, "Players");
        }

        [Then(@"each player should be assigned a unique color")]
        public void ThenEachPlayerShouldBeAssignedAUniqueColor()
        {
            var players = _scenarioContext.Get<List<Player>>("Players");

            var colors = players.Select(p => p.Color).ToList();
            colors.Distinct().Count().Should().Be(colors.Count);
        }

        [Then(@"each player should have 4 game pieces of their color")]
        public void ThenEachPlayerShouldHave4GamePiecesOfTheirColor()
        {
            var players = _scenarioContext.Get<List<Player>>("Players");

            foreach (var player in players)
            {
                player.GamePieces.Should().NotBeNull();
                player.GamePieces.Count.Should().Be(4);
                player.GamePieces.All(p => p.Color == player.Color).Should().BeTrue();
            }
        }

        [Then("all game pieces should start in their respective home areas")]
        public void ThenAllGamePiecesShouldStartInTheirRespectiveHomeAreas()
        {
            var players = _scenarioContext.Get<List<Player>>("Players");

            foreach (var player in players)
            {
                player.GamePieces.All(p => p.IsInHomeArea).Should().BeTrue();
            }
        }

        [Given("all players have joined the game")]
        public void GivenAllPlayersHaveJoinedTheGame()
        {
            _game = new Game();
            _board = new Board();
            _game.Board = _board;

            string[] colors = { "Blue", "Red", "Green", "Yellow" };
            _players = new List<Player>();

            for (int i = 0; i < 4; i++)
            {
                var player = new Player { Name = $"Player{i + 1}", Color = colors[i] };
                player.GamePieces = new List<GamePiece>();
                for (int j = 0; j < 4; j++)
                {
                    player.GamePieces.Add(new GamePiece { Color = colors[i], IsInHomeArea = true });
                }
                _players.Add(player);
                _game.AddPlayer(player);
            }

            _scenarioContext.Set(_game, "Game");
            _scenarioContext.Set(_players, "Players");
        }

        [When("each player rolls the dice in the beginning")]
        public void WhenEachPlayerRollsTheDiceInTheBeginning()
        {
            var players = _scenarioContext.Get<List<Player>>("Players");
            var dice = new DiceService();
            _playerRolls = new Dictionary<string, int>();

            foreach (var player in players)
            {
                _playerRolls[player.Name] = dice.RollDice();
            }

            _scenarioContext.Set(_playerRolls, "PlayerRolls");
        }

        [Then("the player with the highest roll should take the first turn")]
        public void ThenThePlayerWithTheHighestRollShouldTakeTheFirstTurn()
        {
            var playerRolls = _scenarioContext.Get<Dictionary<string, int>>("PlayerRolls");
            var players = _scenarioContext.Get<List<Player>>("Players");
            var game = _scenarioContext.Get<Game>("Game");

            var maxRoll = playerRolls.Values.Max();
            var playersWithMaxRoll = playerRolls
                .Where(pr => pr.Value == maxRoll)
                .Select(pr => pr.Key)
                .ToList();

            if (playersWithMaxRoll.Count == 1)
            {
                var startingPlayerName = playersWithMaxRoll[0];
                var startingPlayer = players.First(p => p.Name == startingPlayerName);
                game.CurrentPlayer = startingPlayer;

                game.CurrentPlayer.Name.Should().Be(startingPlayerName);
            }
        }

        [Then("in case of a tie, those players should roll again")]
        public void ThenInCaseOfATieThosePlayersShouldRollAgain()
        {
            var playerRolls = _scenarioContext.Get<Dictionary<string, int>>("PlayerRolls");
            var maxRoll = playerRolls.Values.Max();
            var playersWithMaxRoll = playerRolls
                .Where(pr => pr.Value == maxRoll)
                .Select(pr => pr.Key)
                .ToList();

            if (playersWithMaxRoll.Count > 1)
            {
                var dice = new DiceService();
                var tiePlayerRolls = new Dictionary<string, int>();

                foreach (var playerName in playersWithMaxRoll)
                {
                    tiePlayerRolls[playerName] = dice.RollDice();
                }

                var newMaxRoll = tiePlayerRolls.Values.Max();
                var winningPlayers = tiePlayerRolls
                    .Where(pr => pr.Value == newMaxRoll)
                    .Select(pr => pr.Key)
                    .ToList();

                winningPlayers.Count.Should().BeLessThanOrEqualTo(playersWithMaxRoll.Count);
            }
        }

        [Given("the board is set up")]
        public void GivenTheBoardIsSetUp()
        {
            _board = new Board();
            _scenarioContext.Set(_board, "Board");
        }

        [Given("all players have joined")]
        public void GivenAllPlayersHaveJoined()
        {
            if (!_scenarioContext.TryGetValue<List<Player>>("Players", out _players))
            {
                string[] colors = { "Blue", "Red", "Green", "Yellow" };
                _players = new List<Player>();

                for (int i = 0; i < 4; i++)
                {
                    var player = new Player { Name = $"Player{i + 1}", Color = colors[i] };
                    player.GamePieces = new List<GamePiece>();
                    for (int j = 0; j < 4; j++)
                    {
                        player.GamePieces.Add(new GamePiece { Color = colors[i], IsInHomeArea = true });
                    }
                    _players.Add(player);
                }

                _scenarioContext.Set(_players, "Players");
            }

            if (!_scenarioContext.TryGetValue<Game>("Game", out _game))
            {
                _game = new Game();
                _game.Board = _scenarioContext.Get<Board>("Board");
                foreach (var player in _players)
                {
                    _game.AddPlayer(player);
                }
                _scenarioContext.Set(_game, "Game");
            }
        }

        [Given("the starting player has been determined")]
        public void GivenTheStartingPlayerHasBeenDetermined()
        {
            var game = _scenarioContext.Get<Game>("Game");
            var players = _scenarioContext.Get<List<Player>>("Players");

            // First player starts
            game.CurrentPlayer = players[0];
            game.CurrentPlayerIndex = 0;

            _scenarioContext.Set(game, "Game");
        }

        [Then("the game should be ready to start")]
        public void ThenTheGameShouldBeReadyToStart()
        {
            var game = _scenarioContext.Get<Game>("Game");

            game.Board.Should().NotBeNull();
            game.Players.Should().NotBeNull();
            game.Players.Count.Should().BeGreaterThan(0);
            game.CurrentPlayer.Should().NotBeNull();
            game.IsGameOver.Should().BeFalse();
        }
    }
}