using System;
using System.Collections.Generic;

using static RobotSumo.Core.Sensors.InfraRedSensorReadEnum;
using static RobotSumo.Core.Sensors.UltraSonicSensorReadEnum;

namespace RobotSumo.Core
{
    public static class RobotStateFactory
    {
        public static States Create(Action attack, Action moveFree,
            Action actionTwo,
            Action actionThree,
            Action actionFour,
            Action noAction) => new States(new List<Robot.State>
        {
            new Robot.State(Black, Black, Something, r => attack()),
            new Robot.State(Black, Black, Nothing, r => moveFree()),
            new Robot.State(White, Black, Something, r => noAction()),
            new Robot.State(White, Black, Nothing, r => actionTwo()),
            new Robot.State(Black, White, Something, r => noAction()),
            new Robot.State(Black, White, Nothing, r => actionThree()),
            new Robot.State(White, White, Something, r => noAction()),
            new Robot.State(White, White, Nothing, r => actionFour())
        });
    }
}