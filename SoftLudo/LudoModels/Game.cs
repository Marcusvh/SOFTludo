using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoModels
{
    public class Game
    {
        public Board Board { get; set; }
        public List<Player> Players { get; set; }
        public Player CurrentPlayer { get; set; }
        public int CurrentPlayerIndex { get; set; }
        public bool IsGameOver { get; private set; }
        public Player Winner { get; private set; }

        // Create PlayerService class and move the logic
        public void AddPlayer(Player player)
        {
            if (Players == null)
            {
                Players = new List<Player>();
            }
            Players.Add(player);
        }

    }
}
