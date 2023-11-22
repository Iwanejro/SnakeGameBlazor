using Moq;
using NUnit.Framework;
using SnakeGameBlazor.Constants;
using SnakeGameBlazor.Contracts;
using SnakeGameBlazor.Data;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SnakeGameBlazorTests
{
    [TestFixture]
    public class SnakeTests
    {
        private Snake _cut;
        private Mock<ICells> _cellsMock;
        private const int _initialLength = 3;
        private const int _initialSpeed = 300;

        [SetUp]
        public void SnakeTestsSetUp()
        {
            _cut = new Snake(_initialLength, _initialSpeed);
            _cellsMock = new Mock<ICells>();
        }

        public void AddSampleSnakeCells()
        {
            _cut.SnakeCells.AddRange(new List<Cell>
            {
                new Cell { x = 3, y = 4 },
                new Cell { x = 2, y = 4 },
                new Cell { x = 2, y = 3 },
                new Cell { x = 2, y = 2 }
            });
            _cut.Length = 4;
        }

        [Test]
        public void InitializeSnake_IfThereAreAnyExistingSnakeCells_ShouldClearSnakeCellsBeforeCreatingNewOnes()
        {
            //Arrange
            _cut.SnakeCells.Add(new Cell
            {
                x = 1,
                y = 2,
                Color = GridColors.Blue
            });
            var expectedSnakeCellsCount = _cut.Length;
            _cellsMock.Setup(mock => mock.Get(It.IsAny<int>(), It.IsAny<int>())).Returns(new Cell());

            //Act
            _cut.InitializeSnake(10, _cellsMock.Object);

            //Assert
            Assert.AreEqual(expectedSnakeCellsCount, _cut.SnakeCells.Count);
        }

        [Test]
        public void InitializeSnake_WhenCalled_ShouldAddANumberOfSnakeCellsEqualToTheSnakeInitialLength()
        {
            //Arrange
            _cellsMock.Setup(mock => mock.Get(It.IsAny<int>(), It.IsAny<int>())).Returns(new Cell());

            //Act
            _cut.InitializeSnake(10, _cellsMock.Object);

            //Assert
            Assert.AreEqual(_cut.Length, _cut.SnakeCells.Count);
        }

        [TestCase(5, 3)]
        [TestCase(13, 7)]
        public void InitializeSnake_WhenTheGridSizeIsAnOddNumber_SnakeHeadShouldAppearInTheMiddleOfTheBoard(int gridSize, int expectedPosition)
        {
            //Arrange
            _cellsMock.Setup(mock => mock.Get(It.IsAny<int>(), It.IsAny<int>())).Returns(new Cell());

            //Act
            _cut.InitializeSnake(gridSize, _cellsMock.Object);
            var snakeHeadHorizontalPositionOnTheGameGrid = _cut.Head.x + 1; // as the user sees it

            //Assert
            Assert.AreEqual(expectedPosition, snakeHeadHorizontalPositionOnTheGameGrid);
        }

        [Test]
        public void InitializeSnake_WhenCalled_AtTheBeginningOfTheGameTheSnakesTailShouldBeInTheSamePositionAsTheSnakesHead()
        {
            //Arrange
            _cellsMock.Setup(mock => mock.Get(It.IsAny<int>(), It.IsAny<int>())).Returns(new Cell());

            //Act
            _cut.InitializeSnake(10, _cellsMock.Object);

            //Assert
            Assert.AreEqual(_cut.Head.x, _cut.Tail.x);
            Assert.AreEqual(_cut.Head.y, _cut.Tail.y);
        }

        [Test]
        public void InitializeSnake_WhenCalled_ShouldSetHeadCellColorToEyesDown()
        {
            //Arrange
            _cellsMock.Setup(mock => mock.Get(It.IsAny<int>(), It.IsAny<int>())).Returns(new Cell());

            //Act
            _cut.InitializeSnake(10, _cellsMock.Object);
            var headCell = _cellsMock.Object.Get(_cut.Head.x, _cut.Head.y);

            //Assert
            Assert.AreEqual(GridColors.EyesDown, headCell.Color);
        }

        [Test]
        public void MoveBody_WhenCalled_ShouldSetTheTailPositionToTheLastElementOfSnakeCellsBeforeMoving()
        {
            // Arrange
            AddSampleSnakeCells();
            var lastSnakeCellX = _cut.SnakeCells[_cut.Length - 1].x;
            var lastSnakeCellY = _cut.SnakeCells[_cut.Length - 1].y;

            // Act
            _cut.MoveBody();

            // Assert
            Assert.AreEqual(_cut.Tail.x, lastSnakeCellX);
            Assert.AreEqual(_cut.Tail.y, lastSnakeCellY);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void MoveBody_WhenCalled_ShouldMoveEverySnakeCellExceptHeadToThePositionOfThePreviousCell(int examinedCellNumber)
        {
            // Arrange
            AddSampleSnakeCells();

            var positionOfTheCellPrecedingTheExaminedOneBeforeMoving = new int[2] { _cut.SnakeCells[examinedCellNumber-1].x, _cut.SnakeCells[examinedCellNumber-1].y };

            // Act
            _cut.MoveBody();
            var newPositionOfTheExaminedCell = new int[2] { _cut.SnakeCells[examinedCellNumber].x, _cut.SnakeCells[examinedCellNumber].y };

            // Assert
            Assert.AreEqual(positionOfTheCellPrecedingTheExaminedOneBeforeMoving, newPositionOfTheExaminedCell);
        }

        [Test]
        public void MoveBody_WhenCalled_ShouldNotMoveSnakeHead()
        {
            // Arrange
            AddSampleSnakeCells();

            var snakeHeadPositionBeforeMoving = new int[2] { _cut.SnakeCells[0].x, _cut.SnakeCells[0].y };

            // Act
            _cut.MoveBody();
            var snakeHeadPositionAfterMoving = new int[2] { _cut.SnakeCells[0].x, _cut.SnakeCells[0].y };

            // Assert
            Assert.AreEqual(snakeHeadPositionBeforeMoving, snakeHeadPositionAfterMoving);
        }

        [TestCase(Directions.Up, 0, 1)]
        [TestCase(Directions.Down, 0, -1)]
        [TestCase(Directions.Left, -1, 0)]
        [TestCase(Directions.Right, 1, 0)]
        public void MoveHead_WhenCalled_ShouldMoveTheFirstSnakeCellInTheRightDirection(string direction, int horizontalValue, int verticalValue)
        {
            // Arrange
            AddSampleSnakeCells();
            var snakeHeadPositionBeforeMovement = new int[2] { _cut.Head.x, _cut.Head.y };

            // Act
            _cut.MoveHead(direction);
            var expectedSnakeHeadPositionAfterMovement = new int[2] { 
                snakeHeadPositionBeforeMovement[0] + horizontalValue,
                snakeHeadPositionBeforeMovement[1] + verticalValue 
            };
            var newSnakeHeadPosition = new int[2] { _cut.Head.x, _cut.Head.y };

            // Assert
            Assert.AreEqual(expectedSnakeHeadPositionAfterMovement, newSnakeHeadPosition);
        }

        [TestCase(Directions.Up)]
        [TestCase(Directions.Down)]
        [TestCase(Directions.Left)]
        [TestCase(Directions.Right)]
        public void MoveHead_WhenCalled_ShouldSetTheSnakeHeadInTheRightDirection(string moveDirection)
        {
            // Arrange
            AddSampleSnakeCells();

            // Act
            _cut.MoveHead(moveDirection);
            
            // Assert
            Assert.AreEqual(moveDirection, _cut.HeadDirection);
        }

        [Test]
        public void HideTail_WhenCalled_ShouldSetTheTailColorToGreen()
        {
            //Arrange
            var testCell = new Cell { Color = GridColors.Black };
            _cellsMock.Setup(mock => mock.Get(_cut.Tail.x, _cut.Tail.y)).Returns(testCell);

            //Act
            _cut.HideTail(_cellsMock.Object);

            //Assert
            Assert.AreEqual(GridColors.Green, testCell.Color);
        }

        [Test]
        public void MakeSnakeLonger_WhenCalled_ShouldAddOneSnakeCellAndIncrementTheSnakeLengthBy1()
        {
            // Arrange
            var originalSnakeCellsCount = _cut.SnakeCells.Count();
            var originalSnakeLength = _cut.Length;

            // Act
            _cut.MakeSnakeLonger();

            // Assert
            Assert.AreEqual(originalSnakeCellsCount + 1, _cut.SnakeCells.Count());
            Assert.AreEqual(originalSnakeLength + 1, _cut.Length);
        }

        [Test]
        public void MakeSnakeLonger_WhenCalled_TheLastSnakeCellShouldHaveTheSameCoordinatesAsTheSnakeTail()
        {
            // Arrange
            var snakeTailCoordinates = new int[2] {_cut.Tail.x, _cut.Tail.y };

            // Act
            _cut.MakeSnakeLonger();
            var lastSnakeCellCoordinates = new int[2] { _cut.SnakeCells[_cut.SnakeCells.Count - 1].x, _cut.SnakeCells[_cut.SnakeCells.Count - 1].y };

            // Assert
            Assert.AreEqual(snakeTailCoordinates, lastSnakeCellCoordinates);
        }

        [TestCase(Directions.Up, GridColors.EyesUp)]
        [TestCase(Directions.Down, GridColors.EyesDown)]
        [TestCase(Directions.Left, GridColors.EyesLeft)]
        [TestCase(Directions.Right, GridColors.EyesRight)]
        public void ChangeSnakeHeadPicture_WhenCalled_ShouldSetTheCorrectSnakeHeadColorDependingOnTheHeadDirection(string headDirection, string expectedHeadColor)
        {
            // Arrange
            AddSampleSnakeCells();
            _cut.HeadDirection = headDirection;
            var testCell = new Cell { Color = GridColors.Red };
            _cellsMock.Setup(mock => mock.Get(_cut.Head.x, _cut.Head.y)).Returns(testCell);

            // Act
            _cut.ChangeSnakeHeadPicture(_cellsMock.Object);

            // Assert
            Assert.AreEqual(expectedHeadColor, testCell.Color);
        }
    }

    // Arrange


    // Act


    // Assert

}