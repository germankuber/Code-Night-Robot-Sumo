using System;

using FluentAssertions;

using Moq;

using Xunit;

namespace RobotSumo.Test
{
    public class RobotShould
    {
        private readonly Mock<IInfraRedSensorDriver> _frontBackSensorMock = new Mock<IInfraRedSensorDriver>();
        private readonly Mock<IInfraRedSensorDriver> _backBackSensorMock = new Mock<IInfraRedSensorDriver>();
        private readonly Mock<IUltraSonicSensorDriver> _ultraSonicSensorDriverMock = new Mock<IUltraSonicSensorDriver>();
        private readonly Mock<IMoveFree> _moveFreeMock = new Mock<IMoveFree>();
        private Robot CreateRobotWithoutSensor() => new Robot(null, null, null, null);
        private Robot CreateRobotWithSensor() => new Robot(_frontBackSensorMock.Object,
            _backBackSensorMock.Object,
            _moveFreeMock.Object,
            _ultraSonicSensorDriverMock.Object);


        [Fact]
        public void Execute_Move_Free_If_Back_Black_And_Front_Black()
        {
            _backBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.Black);
            _frontBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.Black);
            _ultraSonicSensorDriverMock.Setup(x => x.Read()).Returns(UltraSonicSensorReadEnum.Nothing);

            var robot = CreateRobotWithSensor();
            robot.Start();

            _moveFreeMock.Verify(x => x.Move(It.IsAny<Robot>()));
        }
        [Fact]
        public void Execute_Attack_If_Back_Black_And_Front_Black()
        {
            _backBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.Black);
            _frontBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.Black);
            _ultraSonicSensorDriverMock.Setup(x => x.Read()).Returns(UltraSonicSensorReadEnum.Something);

            var robot = CreateRobotWithSensor();
            robot.Start();

            robot.RightWheel.State.Should().Be(WheelState.MaxMove);
            robot.LeftWheel.State.Should().Be(WheelState.MaxMove);
        }

        [Fact]
        public void Execute_ActionTwo_If_Back_Black_And_Front_White()
        {
            _backBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.Black);
            _frontBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.White);
            _ultraSonicSensorDriverMock.Setup(x => x.Read()).Returns(UltraSonicSensorReadEnum.Nothing);
            var robot = CreateRobotWithSensor();
            robot.Start();
            robot.RightWheel.State.Should().Be(WheelState.Back);
            robot.LeftWheel.State.Should().Be(WheelState.Back);
        }

        [Fact]
        public void Execute_ActionThree_If_Back_White_And_Front_Black()
        {
            _backBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.White);
            _frontBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.Black);
            _ultraSonicSensorDriverMock.Setup(x => x.Read()).Returns(UltraSonicSensorReadEnum.Nothing);

            var robot = CreateRobotWithSensor();
            robot.Start();
            robot.RightWheel.State.Should().Be(WheelState.Move);
            robot.LeftWheel.State.Should().Be(WheelState.Move);
        }


        [Fact]
        public void Execute_ActionThree_If_Back_White_And_Front_White()
        {
            _backBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.White);
            _frontBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.White);
            _ultraSonicSensorDriverMock.Setup(x => x.Read()).Returns(UltraSonicSensorReadEnum.Nothing);

            var robot = CreateRobotWithSensor();
            robot.Start();
            robot.RightWheel.State.Should().Be(WheelState.Move);
            robot.LeftWheel.State.Should().Be(WheelState.Move);
        }

        [Fact]
        public void Execute_NoAction_If_Back_White_And_Front_White()
        {
            _backBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.White);
            _frontBackSensorMock.Setup(x => x.Read()).Returns(InfraRedSensorReadEnum.White);
            _ultraSonicSensorDriverMock.Setup(x => x.Read()).Returns(UltraSonicSensorReadEnum.Something);

            var robot = CreateRobotWithSensor();
            robot.Start();
            robot.RightWheel.State.Should().Be(WheelState.Move);
            robot.LeftWheel.State.Should().Be(WheelState.Move);
        }




        [Fact]
        public void Move_Forward()
        {
            var robot = CreateRobotWithoutSensor();
            robot.MoveForward();
            robot.LeftWheel.State.Should().Be(WheelState.Move);
            robot.RightWheel.State.Should().Be(WheelState.Move);
        }

        [Fact]
        public void Move_Forward_Max_Power()
        {
            var robot = CreateRobotWithoutSensor();
            robot.MoveForwardMaxPower();
            robot.LeftWheel.State.Should().Be(WheelState.MaxMove);
            robot.RightWheel.State.Should().Be(WheelState.MaxMove);
        }

        [Fact]
        public void Move_Back()
        {
            var robot = CreateRobotWithoutSensor();
            robot.MoveBack();
            robot.LeftWheel.State.Should().Be(WheelState.Back);
            robot.RightWheel.State.Should().Be(WheelState.Back);
        }

        [Fact]
        public void Move_Right()
        {
            var robot = CreateRobotWithoutSensor();
            robot.MoveRight();
            robot.LeftWheel.State.Should().Be(WheelState.Stop);
            robot.RightWheel.State.Should().Be(WheelState.Move);
        }

        [Fact]
        public void Move_Left()
        {
            var robot = CreateRobotWithoutSensor();
            robot.MoveLeft();
            robot.LeftWheel.State.Should().Be(WheelState.Move);
            robot.RightWheel.State.Should().Be(WheelState.Stop);
        }

    }
}


//var game = new Tablero(german, gabi);

//while (game.turn() || german.win() || barcho.win() || game.tie())
//{​​

//if (german.detect())
//{​​
//    german.fullpower();

//}​​else
//{​​
//    german.free();
//}​​

//if (gabi.detect())
//{​​
//    gabi.fullpower();

//}​​else
//{​​
//    gabi.freemove();
//}
//}​​