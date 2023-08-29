using SnakeGameBlazor.Data;

namespace SnakeGameBlazor.Contracts
{
    public interface ICells
    {
        Cell Get(int x, int y);
        bool Any(string gridColor);
        Task MakeCellsGreen();
        void MakeEdgeBlack();
        Task Initialize(int gridSize);
    }
}
