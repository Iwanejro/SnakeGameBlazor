using Moq;
using NUnit.Framework;
using SnakeGameBlazor.Constants;
using SnakeGameBlazor.Contracts;
using SnakeGameBlazor.Data;

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

        public void MoveHead_WhenCalled_ShouldMoveTheFirstSnakeCellInTheRightDirection(string direction)
        {

        }

    }

    // Arrange


    // Act


    // Assert

}