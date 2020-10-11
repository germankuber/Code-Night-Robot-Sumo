using RobotSumo.Core.Sensors;

namespace RobotSumo.Core
{
    public class Drivers
    {
        public Drivers(IInfraRedSensorDriver infraRedSensorDriverFront, IInfraRedSensorDriver infraRedSensorDriverBack, IUltraSonicSensorDriver ultraSonicSensorDriver)
        {
            InfraRedSensorDriverFront = infraRedSensorDriverFront;
            InfraRedSensorDriverBack = infraRedSensorDriverBack;
            UltraSonicSensorDriver = ultraSonicSensorDriver;
        }

        public IInfraRedSensorDriver InfraRedSensorDriverFront { get; }
        public IInfraRedSensorDriver InfraRedSensorDriverBack { get; }
        public IUltraSonicSensorDriver UltraSonicSensorDriver { get; }
    }
}