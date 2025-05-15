using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using LudoModels;
using LudoModels.Dtos;
using LudoModels.Requests;
using System.Collections.Generic;
using LudoModels.Responses;
using Moq;
using Newtonsoft.Json;
using System.Net;
using Moq.Protected;
using System.Text;

namespace LudoRegnroll.StepDefinitions
{
    [Binding]
    public class GameManagementStepDefinitions
    {
        private readonly HttpClient _httpClient;
        private HttpResponseMessage _response;
        private GameResponse _gameResponse;
        private List<GameResponse> _games;
        private PlayerResponseDto? _currentPlayer;
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private int gameId; // Tilføj i en model

        public GameManagementStepDefinitions()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost")
            };
        }

        // Hjælpe metode til at test respons med indhold, altså for success status kode
        private void SetupMockResponse<T>(HttpMethod method, string url, HttpStatusCode statusCode, T content)
        {
            var jsonContent = JsonConvert.SerializeObject(content);

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == method &&
                        req.RequestUri.PathAndQuery == url),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
                });
        }

        // Hjæpe metode til at test 404 or 500 status kode respons
        // Uden T content i parameter og jsonContent
        private void SetupMockResponse(HttpMethod method, string url, HttpStatusCode statusCode)
        {
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == method &&
                        req.RequestUri.PathAndQuery == url),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
                });
        }

        [Given("at spiltjenesten er tilgængelig")]
        public void GivenSpiltjenesteErTilgaengelig()
        {
            SetupMockResponse(HttpMethod.Get, "/startgame", HttpStatusCode.OK);
            var startResponse = _httpClient.GetAsync("/startgame").Result;
            Assert.Equal(HttpStatusCode.OK, startResponse.StatusCode); // Skal måske have sin egen Then
        }

        [When("jeg anmoder om alle spil")]
        public async Task WhenAnmoderOmAllespil()
        {
            var mockGames = new List<GameResponse>
            {   
                new GameResponse { Id = 1, State = GameState.Created.ToString(), Players = new List<Player>() },
                new GameResponse { Id = 2, State = GameState.Running.ToString(), Players = new List<Player>() }
            };

            SetupMockResponse(HttpMethod.Get, "/games", HttpStatusCode.OK, mockGames);

            _response = await _httpClient.GetAsync("/games");
            _games = JsonConvert.DeserializeObject<List<GameResponse>>(_response.Content.ReadAsStringAsync().Result);
        }

        [Then("bør jeg modtage en liste over spil")]
        public void ThenModtageListeOverSpil()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
            Assert.NotNull(_games);
            Assert.NotEmpty(_games);
        }

        [Given("at et spil med id {int} eksisterer")]
        public async Task GivenSpilEksisterer(int gameId)
        {
            var customGame = new GameResponse
            {
                Id = gameId,
                State = GameState.Created.ToString(),
                Players = new List<Player>
                {
                    new Player { Id = 1, Name = "PlayerRed" }
                }
            };

            // Opretter en request med dicerse værdier
            SetupMockResponse(HttpMethod.Get, $"/games/{gameId}",HttpStatusCode.OK,customGame);

            // Gemmer respons i _gameResponse
            _response = _httpClient.GetAsync($"/games/{gameId}").Result;
            _gameResponse = JsonConvert.DeserializeObject<GameResponse>(_response.Content.ReadAsStringAsync().Result);

        }

        [Given("at intet spil med id {int} eksisterer")]
        public async Task GivenIntetSpilEksisterer(int gameId)
        {
            SetupMockResponse(HttpMethod.Get, $"/games/{gameId}", HttpStatusCode.NoContent);

            _response = _httpClient.GetAsync($"/games/{gameId}").Result;
            Assert.Equal(HttpStatusCode.NoContent, _response.StatusCode);
        }

        [When(@"jeg anmoder om spil med id {int}")]
        public async Task WhenAnmoderOmSpil(int gameId)
        {
            // Få det enkelte spil
            _response = await _httpClient.GetAsync($"/games/{gameId}");
        }

        [Then("bør jeg modtage spillets detaljer")]
        public void ThenModtageSpilDetaljer()
        {
            // Eksisterer spil
            Assert.NotNull(_gameResponse);
            // Spillets tilstand
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
            // Er det blevet deserialiseret korrekt
            _gameResponse = JsonConvert.DeserializeObject<GameResponse>(_response.Content.ReadAsStringAsync().Result);
            Assert.NotNull(_gameResponse);
        }

        [Then("bør jeg ikke modtage noget indhold")]
        public void ThenIngenIndhold()
        {
            Assert.Equal(HttpStatusCode.NoContent, _response.StatusCode);
        }

        [Given("en spiller med id {int} eksisterer")]
        public async Task GivenSpillerEksistererAsync(int playerId)
        {
            var player = new PlayerResponseDto
            {
                Id = playerId,
                Name = $"Player{playerId}"
            };

            SetupMockResponse(HttpMethod.Get, $"/players/{playerId}", HttpStatusCode.OK, player);

            var playerResponse = _httpClient.GetAsync($"/players/{playerId}").Result;
            playerResponse.EnsureSuccessStatusCode();

            _currentPlayer = JsonConvert.DeserializeObject<PlayerResponseDto>(playerResponse.Content.ReadAsStringAsync().Result);

            Assert.NotNull(_currentPlayer);
            Assert.Equal(playerId, _currentPlayer.Id);
        }

        [Given("en spiller med id {int} er i spillet")]
        public void GivenEnSpillerMedIdErISpillet(int playerId)
        {
            Assert.NotNull(_gameResponse);

            var playerExists = _gameResponse.Players.Any(p => p.Id == playerId);

            if (!playerExists)
            {
                _gameResponse.Players.Add(new Player { Id = playerId, Name = $"Player{playerId}" });
            }

            var playerInGame = _gameResponse.Players.FirstOrDefault(p => p.Id == playerId);
            Assert.NotNull(playerInGame); // then
        }


        [When("jeg opretter et nyt spil med spiller id {int}")]
        public async Task WhenOpretterNytSpil(int playerId)
        {
            var newGame = new GameResponse
            {
                Id = 999, // New game ID
                State = GameState.Created.ToString(),
                Players = new List<Player>
                {
                    new Player { Id = playerId, Name = $"Player{playerId}" }
                }
            };

            SetupMockResponse(HttpMethod.Post, "/games", HttpStatusCode.Created, newGame);

            _response = await _httpClient.PostAsJsonAsync("/games", playerId);
            _gameResponse = JsonConvert.DeserializeObject<GameResponse>(_response.Content.ReadAsStringAsync().Result);
        }

        [Then("bør spillet oprettes succesfuldt")]
        public async Task ThenSpilOprettetSuccesfuldt()
        {
            Assert.True(_response.IsSuccessStatusCode);
            Assert.NotNull(_gameResponse);
        }

        [Then("spiller {int} bør være i spillet")]
        public void ThenSpillerISpilletAsync(int playerId)
        {
            Assert.Contains(_gameResponse.Players, p => p.Id == playerId);
        }

        [When("spiller {int} deltager i spil {int}")]
        public async Task WhenSpillerDeltagerISpil(int playerId, int gameId)
        {
            var updatedGame = new GameResponse
            {
                Id = gameId,
                State = GameState.Created.ToString(),
                Players = new List<Player>
                {
                    new Player { Id = 1, Name = "ExistingPlayer" },
                    new Player { Id = playerId, Name = $"Player{playerId}" }
                }
            };

            SetupMockResponse(HttpMethod.Post, $"/games/{gameId}/join", HttpStatusCode.OK, updatedGame);

            _response = await _httpClient.PostAsJsonAsync($"/games/{gameId}/join", playerId);
            _gameResponse = JsonConvert.DeserializeObject<GameResponse>(_response.Content.ReadAsStringAsync().Result);
        }

        [Then("bør spil {int} inkludere spiller {int}")]
        public async Task ThenSpilInkludererSpiller(int gameId, int playerId)
        {
            Assert.True(_response.IsSuccessStatusCode);
            Assert.Contains(_gameResponse.Players, p => p.Id == playerId);
        }

        [When("spiller {int} forsøger at deltage i spil {int}")]
        public async Task WhenSpillerForsoegerDeltage(int playerId, int gameId)
        {
            // Bad request response
            SetupMockResponse(HttpMethod.Post, $"/games/{gameId}/join", HttpStatusCode.BadRequest);

            _response = await _httpClient.PostAsJsonAsync($"/games/{gameId}/join", playerId);
        }

        [Then("bør der returneres en fejl")]
        public void ThenReturneretFejl()
        {
            Assert.False(_response.IsSuccessStatusCode);
        }

        [Given("spillet har nok spillere")]
        public void GivenSpilletHarNokSpillere()
        {
            // Skal have mindst 2 spillere
            if (_gameResponse.Players.Count < 2)
            {
                _gameResponse.Players.Add(new Player { Id = 999, Name = "ExtraPlayer" });
            }
        }

        [When("spiller {int} starter spil {int}")]
        public async Task WhenStarterSpil(int playerId, int gameId)
        {
            var startedGame = new GameResponse
            {
                Id = gameId,
                State = GameState.Running.ToString(),
                CurrentPlayerId = playerId,
                Players = _gameResponse?.Players ?? new List<Player>
                {
                    new Player { Id = playerId, Name = $"Player{playerId}" },
                    new Player { Id = 999, Name = "AnotherPlayer" }
                }
            };

            SetupMockResponse(HttpMethod.Post, $"/games/{gameId}/start", HttpStatusCode.OK, startedGame);

            _response = await _httpClient.PostAsJsonAsync($"/games/{gameId}/start", playerId);
            _gameResponse = JsonConvert.DeserializeObject<GameResponse>(_response.Content.ReadAsStringAsync().Result);
        }

        [Then("bør spil {int} være i gang")]
        public async Task ThenSpilIGang(int gameId)
        {
            Assert.True(_gameResponse.Players.Count >= 2);
            Assert.True(_response.IsSuccessStatusCode);
            Assert.NotNull(_gameResponse);
            Assert.Equal(GameState.Running.ToString(), _gameResponse.State);
        }

        [Given("at et spil med id {int} er i gang")]
        public void GivenSpilIGang(int gameId)
        {
            var runningGame = new GameResponse
            {
                Id = gameId,
                State = GameState.Running.ToString(),
                CurrentPlayerId = 1,
                Players = new List<Player>
                {
                    new Player { Id = 1, Name = "PlayerGreen" },
                    new Player { Id = 2, Name = "PlayerBlue" }
                }
            };

            SetupMockResponse(HttpMethod.Get, $"/games/{gameId}", HttpStatusCode.OK, runningGame);

            _response = _httpClient.GetAsync($"/games/{gameId}").Result;
            _gameResponse = JsonConvert.DeserializeObject<GameResponse>(_response.Content.ReadAsStringAsync().Result);

        }

        [Given("det er spiller {int} tur")]
        public void GivenSpillersTur(int playerId)
        {
            _gameResponse.CurrentPlayerId = playerId;
            Assert.Equal(playerId, _gameResponse.CurrentPlayerId);
        }

        [When("spiller {int} kaster terningen i spil {int}")]
        public async Task WhenKasterTerningen(int playerId, int gameId)
        {
            var gameAfterRoll = new GameResponse
            {
                Id = gameId,
                State = GameState.Running.ToString(),
                CurrentPlayerId = playerId,
                LastRoll = 4, 
                Players = _gameResponse?.Players ?? new List<Player>()
            };

            SetupMockResponse(HttpMethod.Post, $"/games/{gameId}/roll", HttpStatusCode.OK, gameAfterRoll);

            _response = await _httpClient.PostAsJsonAsync($"/games/{gameId}/roll", playerId);
            _gameResponse = JsonConvert.DeserializeObject<GameResponse>(_response.Content.ReadAsStringAsync().Result);
        }

        [Then("bør terningekastets resultat returneres")]
        public void ThenTerningekastReturneresSuccesfuldt()
        {
            Assert.Equal(GameState.Running.ToString(), _gameResponse.State);
            Assert.True(_response.IsSuccessStatusCode);
            Assert.True(_gameResponse.LastRoll > 0 && _gameResponse.LastRoll <= 6);
        }

        [Then("spillets tilstand bør opdateres")]
        public void ThenSpilletsTilstandOpdateres()
        {
            Assert.NotNull(_gameResponse);
        }

        [Given("spiller {int} har kastet terningen")]
        public void GivenSpillerHarKastetTerningen(int playerId)
        {
            _gameResponse.LastRoll = 5;
            _gameResponse.CurrentPlayerId = playerId;

        }

        [When("spiller {int} spiller en brik i spil {int}")]
        public async Task WhenSpillerBrik(int playerId, int gameId)
        {
            var playTurnRequest = new PlayTurnRequest
            {
                PlayerId = playerId,
                Command = Command.MovePiece
            };

            var gameAfterMove = new GameResponse
            {
                Id = gameId,
                State = GameState.Running.ToString(),
                CurrentPlayerId = (playerId == 1) ? 2 : 1, // Næste spiller
                LastRoll = _gameResponse?.LastRoll ?? 0,
                Players = _gameResponse?.Players ?? new List<Player>()
            };

            SetupMockResponse(HttpMethod.Post, $"/games/{gameId}/play", HttpStatusCode.OK, gameAfterMove);

            _response = await _httpClient.PostAsJsonAsync($"/games/{gameId}/play", playTurnRequest);
            _gameResponse = JsonConvert.DeserializeObject<GameResponse>(_response.Content.ReadAsStringAsync().Result);
        }

        [Then(@"bør brikken flyttes tilsvarende")]
        public void ThenBrikkenFlyttes()
        {
            Assert.True(_gameResponse.LastRoll > 0);
            Assert.True(_response.IsSuccessStatusCode);
            Assert.NotNull(_gameResponse);
        }

        [When("spiller {int} forsøger et ugyldigt træk i spil {int}")]
        public async Task WhenUgyldigtTræk(int playerId, int gameId)
        {
            var playTurnRequest = new PlayTurnRequest
            {
                PlayerId = playerId,
                Command = Command.InvalidMove
            };

            // Invalid move response
            SetupMockResponse(HttpMethod.Post, $"/games/{gameId}/play", HttpStatusCode.BadRequest);

            _response = await _httpClient.PostAsJsonAsync($"/games/{gameId}/play", playTurnRequest);
        }

        [Then("spillets tilstand bør forblive uændret")]
        public void ThenSpilletForbliveUændret()
        {
            Assert.False(_response.IsSuccessStatusCode);
        }
    }
}