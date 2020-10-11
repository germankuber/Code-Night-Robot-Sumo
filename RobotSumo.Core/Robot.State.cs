using System;

using RobotSumo.Core.Sensors;

namespace RobotSumo.Core
{
    public partial class Robot
    {
        public class State
        {
            public InfraRedSensorReadEnum FrontInfraSensor { get; set; }
            public InfraRedSensorReadEnum BackInfraSensor { get; set; }
            public UltraSonicSensorReadEnum UltraSonicSensor { get; set; }
            public Action<Robot> Action { get; set; }

            public State(InfraRedSensorReadEnum frontInfraSensor,
                InfraRedSensorReadEnum backInfraSensor,
                UltraSonicSensorReadEnum ultraSonicSensor,
                Action<Robot> action)
            {
                FrontInfraSensor = frontInfraSensor;
                BackInfraSensor = backInfraSensor;
                UltraSonicSensor = ultraSonicSensor;
                Action = action;
            }

            public bool Check(Robot robot) =>
                robot.FrontSensor.Read() == FrontInfraSensor
                &&
                robot.BackSensor.Read() == BackInfraSensor
                &&
                robot.UltrasonicSensor.Read() == UltraSonicSensor;

        }
    }
}