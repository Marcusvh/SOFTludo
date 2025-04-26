using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoModels.BoardSpec
{
    public class GoalLane
    {
        public string Color { get; set; }
        public List<Space> Spaces { get; set; }
    }
}
