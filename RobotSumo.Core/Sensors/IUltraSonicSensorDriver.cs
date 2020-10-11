namespace RobotSumo.Core.Sensors
{
    public interface IUltraSonicSensorDriver
    {
        public UltraSonicSensorReadEnum Read();
    }
}