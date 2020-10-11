using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using RobotSumo.Core.Sensors;

using static RobotSumo.Core.Sensors.InfraRedSensorReadEnum;
using static RobotSumo.Core.Sensors.UltraSonicSensorReadEnum;
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

        public IInfraRedSensorDriver InfraRedSensorDriverFront { get; private set; }
        public IInfraRedSensorDriver InfraRedSensorDriverBack { get; private set; }
        public IUltraSonicSensorDriver UltraSonicSensorDriver { get; private set; }
    }

    public partial class Robot : IRobot
    {
        private readonly List<State> _states = new List<State>
        {
            new State(Black, Black, Something, r => r.Attack()),
            new State(Black, Black, Nothing, r => r.MoveFree()),
            new State(White, Black, Something, r => r.NoAction()),
            new State(White, Black, Nothing, r => r.ActionTwo()),
            new State(Black, White, Something, r => r.NoAction()),
            new State(Black, White, Nothing, r => r.ActionThree()),
            new State(White, White, Something, r => r.NoAction()),
            new State(White, White, Nothing, r => r.ActionFour())
        };

        private readonly IMoveFree _moveFree;
        private readonly Action _noActionNotification;
        private readonly Action _actionNotification;
        public Wheel RightWheel { get; } = new Wheel();
        public Wheel LeftWheel { get; } = new Wheel();

        public InfraRedSensor FrontSensor { get; }
        public InfraRedSensor BackSensor { get; }
        public UltrasonicSensor UltrasonicSensor { get; }

        public Robot(Drivers drivers,
            IMoveFree moveFree,
            Action noActionNotification,
            Action actionNotification)
        {
            _moveFree = moveFree;
            _noActionNotification = noActionNotification;
            _actionNotification = actionNotification;
            UltrasonicSensor = new UltrasonicSensor(drivers.UltraSonicSensorDriver);
            FrontSensor = new InfraRedSensor(drivers.InfraRedSensorDriverFront);
            BackSensor = new InfraRedSensor(drivers.InfraRedSensorDriverBack);
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

        private void MoveFree()
        {
            _moveFree.Move(this);
            _actionNotification?.Invoke();
        }

        private void ActionTwo()
        {
            RightWheel.Stop();
            LeftWheel.Stop();
            MoveBack();
            _actionNotification?.Invoke();
        }

        private void ActionThree()
        {
            RightWheel.Stop();
            LeftWheel.Stop();
            MoveForward();
            _actionNotification?.Invoke();
        }

        private void ActionFour()
        {
            MoveLeft();
            MoveForward();
            _actionNotification?.Invoke();
        }
        private void Attack()
        {
            MoveForwardMaxPower();
            _actionNotification?.Invoke();
        }

        private void NoAction() => _noActionNotification?.Invoke();


        public void Do() => _states.Execute(this);
    }
}



