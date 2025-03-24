namespace SoftLudoAPI.Interfaces
{
    public interface IGameService
    {
        void StartGame(List<string> playerNames);
        void PlayTurn();
    }
}
