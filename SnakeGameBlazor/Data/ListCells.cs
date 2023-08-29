using SnakeGameBlazor.Constants;
using SnakeGameBlazor.Contracts;

namespace SnakeGameBlazor.Data
{
    public class ListCells : ICells
    {
        public ListCells(int gridSize)
        {
            _gridSize = gridSize;
            _cells = new List<Cell>();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    _cells.Add(new Cell
                    {
                        x = i,
                        y = j,
                        Color = GridColors.Green
                    });
                }
            }
        }

        private List<Cell> _cells;
        private int _gridSize;

        public async Task MakeCellsGreen()
        {
            var cells = _cells.Where(c => c.x >= 1 && c.y >= 1 && c.x <= _gridSize - 2 && c.y <= _gridSize - 2);
            foreach (var cell in cells)
            {
                cell.Color = GridColors.Green;
            }
        }

        public void MakeEdgeBlack()
        {
            var cells = _cells.Where(c => c.x == 0 || c.y == 0 || c.x == _gridSize - 1 || c.y == _gridSize - 1);
            foreach (var cell in cells)
            {
                cell.Color = GridColors.Black;
            }
        }

        public Cell Get(int x, int y)
        {
            return _cells.Find(c => c.x == x && c.y == y);
        }

        public bool Any(string gridColor)
        {
            return _cells.Any(cell => cell.Color == gridColor);
        }

        public async Task Initialize(int gridSize)
        {
            _gridSize = gridSize;

            if (_cells.Count>0)
                _cells.Clear();

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    _cells.Add(new Cell
                    {
                        x = i,
                        y = j,
                        Color = GridColors.Green
                    });
                }
            }
        }
    }
}
