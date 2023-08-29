using SnakeGameBlazor.Constants;
using SnakeGameBlazor.Contracts;

namespace SnakeGameBlazor.Data
{
    public class ArrayCells : ICells
    {
        public ArrayCells(int gridSize)
        {
            _gridSize = gridSize;
        }

        private Cell[,] _cells = new Cell[_gridSize, _gridSize];
        private static int _gridSize;

        public async Task MakeCellsGreen()
        {
            for (int i = 1; i < _gridSize-2; i++)
            {
                for (int j = 1; j < _gridSize-2; j++)
                {
                    _cells[i, j].Color = GridColors.Green;
                }
            }
        }

        public void MakeEdgeBlack()
        {
            for (int i = 0; i <= _gridSize-1; i++)              // bottom
            {
                _cells[i, 0].Color = GridColors.Black;
            }
            for (int i = 0; i <= _gridSize - 1; i++)            // top
            {
                _cells[i, _gridSize-1].Color = GridColors.Black;
            }
            for (int i = 0; i <= _gridSize - 1; i++)            // left
            {
                _cells[0, i].Color = GridColors.Black;
            }
            for (int i = 0; i <= _gridSize - 1; i++)            // right
            {
                _cells[_gridSize-1, i].Color = GridColors.Black;
            }
        }

        public Cell Get(int x, int y)
        {
            return _cells[x, y];
        }

        public bool Any(string gridColor)
        {
            foreach (var cell in _cells)
            {
                if (cell.Color == gridColor)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task Initialize(int gridSize)
        {
            _gridSize = gridSize;
            _cells = new Cell[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    _cells[i, j] = new Cell
                    {
                        x = i,
                        y = j,
                        Color = GridColors.Green
                    };
                }
            }
        }
    }
}
