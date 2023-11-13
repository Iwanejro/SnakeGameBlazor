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


    }
}