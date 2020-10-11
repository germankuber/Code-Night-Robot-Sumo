namespace RobotSumo.Core
{
    public class Player : IPlayer
    {
        private readonly IRobot _robot;
        int _noActionCount = 0;

        public Player(IRobot robot)
        {
            _robot = robot;
        }

        private void NoActionCallback() =>
            ++_noActionCount;

        private void ActionCallback() =>
            _noActionCount = 0;

        public bool Play()
        {
            _robot.Do();
            if (_noActionCount > 10)
                return false;
            return true;

        }
    }
}