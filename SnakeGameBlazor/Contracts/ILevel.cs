using SnakeGameBlazor.Data;

namespace SnakeGameBlazor.Contracts
{
    public interface ILevel
    {
        public int Number { get; set; }
        public int InitialGridSize { get; set; }
        Task PreparePlayingBoard(List<Cell> cells);

    }
}
