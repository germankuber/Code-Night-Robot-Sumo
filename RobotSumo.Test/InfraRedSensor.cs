namespace RobotSumo.Test
{
    public class InfraRedSensor
    {
        private readonly IInfraRedSensorDriver _infraRedSensorDriver;

        public InfraRedSensor(IInfraRedSensorDriver infraRedSensorDriver)
        {
            _infraRedSensorDriver = infraRedSensorDriver;
        }

        public InfraRedSensorReadEnum Read() => _infraRedSensorDriver.Read();
    }
}