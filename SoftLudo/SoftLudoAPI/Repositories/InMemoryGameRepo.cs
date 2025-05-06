using LudoModels;

namespace SoftLudoApi.Repositories;

public class InMemoryGameRepo : IGameRepository
{
    private int nextId = 1;
    private readonly List<Game> games = new List<Game>();

    public Game? GetGame(int id)
    {
        return games.FirstOrDefault(g => g.Id == id);
    }
    public Game CreateGame(Player host)
    {
        var game = new Game{
            Id = nextId++,
            Host = host,
            State = GameState.Lobby,
            Players = new List<Player> { host }
        };
        
        games.Add(game);
        return game;
    }

    public IEnumerable<Game> GetGames()
    {
        return games;
    }

    public Game? UpdateGame(Game game)
    {
        var existingGame = games.FirstOrDefault(g => g.Id == game.Id);
        
        if (existingGame != null)
        {
            existingGame = game;
        }

        return game;
    }
}
