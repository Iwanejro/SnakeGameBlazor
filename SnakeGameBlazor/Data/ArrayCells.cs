using SnakeGameBlazor.Contracts;

namespace SnakeGameBlazor.Data
{
    public class ArrayCells : ICells
    {
        public ArrayCells(int gridSize)
        {
            _cells = new Cell[gridSize, gridSize];
        }

        private Cell[,] _cells;

        public Task MakeCellsGreen()
        {
            throw new NotImplementedException();
        }

        public void MakeEdgeBlack()
        {
            throw new NotImplementedException();
        }

        public Cell Get(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
