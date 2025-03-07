using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoModels
{
    public class GamePiece
    {
        public int Id { get; set; }
        public int Position { get; set; } // maybe have it as a string, so we can write A2, B3, etc. or 04-51 for the board. XY axis based.
        public bool IsHome { get; set; } = true;
        public bool IsGoal { get; set; } = false;
        public bool IsSafe { get; set; } = true;
    }
}
