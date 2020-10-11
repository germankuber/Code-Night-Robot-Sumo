namespace RobotSumo.Core
{
    public class Wheel
    {
        public WheelState State { get; private set; } = WheelState.Stop;
        public void MoveForward()
        {
            State = WheelState.Move;
        }
        public void MoveForwardMaxPower()
        {
            State = WheelState.MaxMove;
        }

        public void MoveBack()
        {
            State = WheelState.Back;
        }

        public void Stop()
        {
            State = WheelState.Stop;
        }
    }
}