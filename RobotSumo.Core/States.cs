using System.Collections.Generic;
using System.Linq;

namespace RobotSumo.Core
{
    public class States
    {
        private readonly List<Robot.State> _states;

        public States(List<Robot.State> states)
        {
            _states = states;
        }
        public  void Execute(Robot robot) => _states.Where(x => x.Check(robot))
            .Select(x => x.Action)
            .First()(robot);
    }
}