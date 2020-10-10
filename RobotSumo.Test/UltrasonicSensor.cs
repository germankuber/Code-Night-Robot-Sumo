namespace RobotSumo.Test
{
    public class UltrasonicSensor
    {
        private readonly IUltraSonicSensorDriver _ultraSonicSensorDriver;

        public UltrasonicSensor(IUltraSonicSensorDriver ultraSonicSensorDriver)
        {
            _ultraSonicSensorDriver = ultraSonicSensorDriver;
        }


        public UltraSonicSensorReadEnum Read() => _ultraSonicSensorDriver.Read();
    }
}