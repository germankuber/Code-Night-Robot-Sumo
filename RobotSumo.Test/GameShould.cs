using FluentAssertions;

using Moq;

using RobotSumo.Core;

using Xunit;

namespace RobotSumo.Test
{
    public class GameShould
    {
        private readonly Mock<IPlayer> _robotOneMock = new Mock<IPlayer>();
        private readonly Mock<IPlayer> _robotTwoMock = new Mock<IPlayer>();

        private Game CreateGame() => new Game(_robotOneMock.Object,
            _robotTwoMock.Object);

        [Fact]
        private void RobotTwo_Winner()
        {
            _robotOneMock.Setup(x => x.Play()).Returns(false);

            var sut = CreateGame();
            sut.Start();
            sut.Loser.Should().Be(_robotOneMock.Object);
            sut.Winner.Should().Be(_robotTwoMock.Object);
        }

        [Fact]
        private void RobotOne_Winner()
        {
            _robotTwoMock.Setup(x => x.Play()).Returns(false);

            _robotOneMock.Setup(x => x.Play()).Returns(true);

            var sut = CreateGame();
            sut.Start();
            sut.Loser.Should().Be(_robotTwoMock.Object);
            sut.Winner.Should().Be(_robotOneMock.Object);
        }
    }
}