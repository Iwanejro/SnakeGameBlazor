namespace SnakeGameBlazor.Data
{
    public class Snake
    {
        public List<Cell> Cells { get; set; }
        public int Length { get; set; }
        public Snake()
        {
            Cells = new List<Cell>();
        }
    }
}
