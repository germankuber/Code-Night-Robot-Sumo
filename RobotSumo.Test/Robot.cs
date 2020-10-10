using System;

namespace RobotSumo.Test
{
    public class Robot
    {
        private readonly IMoveFree _moveFree;
        public Wheel RightWheel { get; } = new Wheel();
        public Wheel LeftWheel { get; } = new Wheel();

        public InfraRedSensor FrontSensor { get; }
        public InfraRedSensor BackSensor { get; }
        public UltrasonicSensor UltrasonicSensor { get; }

        public Robot(IInfraRedSensorDriver infraRedSensorDriverFront,
                     IInfraRedSensorDriver infraRedSensorDriverBack,
                     IMoveFree moveFree,
                     IUltraSonicSensorDriver ultraSonicSensorDriver)
        {
            _moveFree = moveFree;
            UltrasonicSensor = new UltrasonicSensor(ultraSonicSensorDriver);
            FrontSensor = new InfraRedSensor(infraRedSensorDriverFront);
            BackSensor = new InfraRedSensor(infraRedSensorDriverBack);
        }
        public void MoveForward()
        {
            RightWheel.MoveForward();
            LeftWheel.MoveForward();
        }

        public void MoveForwardMaxPower()
        {
            RightWheel.MoveForwardMaxPower();
            LeftWheel.MoveForwardMaxPower();
        }

        public void MoveBack()
        {
            RightWheel.MoveBack();
            LeftWheel.MoveBack();
        }

        public void MoveRight()
        {
            RightWheel.MoveForward();
            LeftWheel.Stop();
        }
        public void MoveLeft()
        {
            RightWheel.Stop();
            LeftWheel.MoveForward();
        }

        private void MoveFree() => _moveFree.Move(this);

        private void ActionTwo()
        {
            RightWheel.Stop();
            LeftWheel.Stop();
            MoveBack();
        }

        private void ActionThree()
        {
            RightWheel.Stop();
            LeftWheel.Stop();
            MoveForward();
        }

        private void ActionFour()
        {
            MoveLeft();
            MoveForward();
        }
        private void Attack()
        {
            MoveForwardMaxPower();
        }

        private void NoAction()
        {
        }

        public void Start()
        {
            if (FrontSensor.Read() == InfraRedSensorReadEnum.Black
                &&
                BackSensor.Read() == InfraRedSensorReadEnum.Black)
                if (UltrasonicSensor.Read() == UltraSonicSensorReadEnum.Something)
                    Attack();
                else
                    MoveFree();

            if (FrontSensor.Read() == InfraRedSensorReadEnum.White
                &&
                BackSensor.Read() == InfraRedSensorReadEnum.Black
            )
                if (UltrasonicSensor.Read() == UltraSonicSensorReadEnum.Something)
                    NoAction();
                else
                    ActionTwo();

            if (FrontSensor.Read() == InfraRedSensorReadEnum.Black
                &&
                BackSensor.Read() == InfraRedSensorReadEnum.White
                )
                if (UltrasonicSensor.Read() == UltraSonicSensorReadEnum.Something)
                    NoAction();
                else
                    ActionThree();

            if (FrontSensor.Read() == InfraRedSensorReadEnum.White
                &&
                BackSensor.Read() == InfraRedSensorReadEnum.White
                )
                if (UltrasonicSensor.Read() == UltraSonicSensorReadEnum.Something)
                    NoAction();
                else
                    ActionFour();
        }
    }
}

//free() => {​​

//sensor(tablero) == [black, black] random();

//sensor(tablero) == [white, black] none()

//sensor(tablero) == [black, white] none()

//sensor(tablero) == [white, white] none()

//}​​



//detect() => {​​

//sensor(tablero) == [red, black]

//}​​