using System;
using RobotSumo.Core.Sensors;

namespace RobotSumo.Core
{
    public partial class Robot : IRobot
    {
        private readonly IMoveFree _moveFree;
        private readonly Action _noActionNotification;
        private readonly Action _actionNotification;
        public Wheel RightWheel { get; } = new Wheel();
        public Wheel LeftWheel { get; } = new Wheel();

        public InfraRedSensor FrontSensor { get; }
        public InfraRedSensor BackSensor { get; }
        public UltrasonicSensor UltrasonicSensor { get; }
        private States States { get;  }
        public Robot(Drivers drivers,
            IMoveFree moveFree,
            CallbackActions callbackActions)
        {
            _moveFree = moveFree;
            _noActionNotification = callbackActions.NoActionNotification;
            _actionNotification = callbackActions.ActionNotification;
            UltrasonicSensor = new UltrasonicSensor(drivers.UltraSonicSensorDriver);
            FrontSensor = new InfraRedSensor(drivers.InfraRedSensorDriverFront);
            BackSensor = new InfraRedSensor(drivers.InfraRedSensorDriverBack);
            States = RobotStateFactory.Create(Attack,
                                              MoveFree,
                                              ActionTwo,
                                              ActionThree,
                                              ActionFour,
                                              NoAction);
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


        public void Do() => States.Execute(this);
    }
}



