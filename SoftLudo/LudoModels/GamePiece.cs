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
        public int Position { get; set; }
        public bool IsHome { get; set; }
        public bool IsGoal { get; set; }
        public bool IsSafe { get; set; }
    }
}
