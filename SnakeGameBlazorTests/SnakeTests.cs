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


    }
}