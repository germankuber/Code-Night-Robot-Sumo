namespace RobotSumo.Core
{
    public class Game
    {
        private readonly IPlayer _playerOne;
        private readonly IPlayer _playerTwo;
        public IPlayer Loser;
        public IPlayer Winner;

        public Game(IPlayer playerOne, IPlayer playerTwo)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
        }
        public void Start()
        {
            while (true)
            {
                if (!_playerOne.Play())
                {
                    Loser = _playerOne;
                    Winner = _playerTwo;
                    break;
                }
                if (!_playerTwo.Play())
                {
                    Loser = _playerTwo;
                    Winner = _playerOne;
                    break;
                }
            }
        }
    }
}