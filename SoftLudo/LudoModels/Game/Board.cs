using LudoModels.BoardSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoModels.Game
{
    public class Board
    {
        public List<Space> RegularSpaces { get; set; }
        public List<HomeArea> HomeAreas { get; set; }
        public List<HomeLane> HomeLanes { get; set; }
        public List<GoalLane> GoalAreas { get; set; }
        public List<GoalLane> SafeHomeBase { get; set; }

    }
}
