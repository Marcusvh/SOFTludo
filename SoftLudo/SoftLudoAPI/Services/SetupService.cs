using LudoModels;

namespace SoftLudoAPI.Services
{
    public class SetupService
    {
        public int PlayerCount { get; set; } = 0;

        public bool Setup(int playerCount)
        {
            return true;
        }

        public string ChoseColour(string colour)
        {
            return "value";
        }

        public Player GetStartingPlayer()
        {
            return new Player();
        }

        
    }
}
