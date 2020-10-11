using System.Collections.Generic;
using System.Linq;

namespace RobotSumo.Core
{
    public static class StatesExtension
    {
        public static void Execute(this List<Robot.State> states,
            Robot robot) => states.Where(x => x.Check(robot))
            .Select(x => x.Action)
            .First()(robot);
    }
}