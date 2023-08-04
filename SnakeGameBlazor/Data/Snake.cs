using SnakeGameBlazor.Constants;
using static System.Formats.Asn1.AsnWriter;

namespace SnakeGameBlazor.Data
{
    public class Snake
    {
        public List<Cell> SnakeCells { get; set; }
        public Cell Head { get; set; }
        public Cell Tail { get; set; }
        public int Length { get; set; }
        public int Speed { get; set; }
        public string HeadDirection { get; set; }

        public static int InitialLenght = 2;
        public static int InitialSpeed = 350;

        public Snake()
        {
            SnakeCells = new List<Cell>();
            Head = new Cell();
            Tail = new Cell();
            Length = InitialLenght;
            Speed = InitialSpeed;
        }

        public void InitializeSnake(int gridSize, List<Cell> cells)
        {
            if (SnakeCells.Count > 0)
            {
                SnakeCells.RemoveAll(c => c.x >= -1);
            }
            Head.x = Convert.ToInt32(Math.Round((double)(gridSize / 2), mode: MidpointRounding.ToEven));
            Head.y = Convert.ToInt32(Math.Round((double)(gridSize / 2), mode: MidpointRounding.ToEven));

            Tail.x = Head.x;
            Tail.y = Head.y;

            for (int i = 0; i < Length; i++)
            {
                SnakeCells.Add(new Cell
                {
                    x = Head.x,
                    y = Head.y
                });

            }
            cells.Find(c => c.x == Head.x && c.y == Head.y).Color = GridColors.EyesDown;
        }

        public void MoveSnakeBody()
        {
            Tail.x = SnakeCells[Length - 1].x;
            Tail.y = SnakeCells[Length - 1].y;

            for (int i = Length - 1; i > 0; i--)
            {
                SnakeCells[i].x = SnakeCells[i - 1].x;
                SnakeCells[i].y = SnakeCells[i - 1].y;
            }
        }

        public void MoveSnakeHead(string direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    SnakeCells[0].y = SnakeCells[0].y + 1;
                    HeadDirection = Directions.Up;
                    break;

                case Directions.Down:
                    SnakeCells[0].y = SnakeCells[0].y - 1;
                    HeadDirection = Directions.Down;
                    break;

                case Directions.Left:
                    SnakeCells[0].x = SnakeCells[0].x - 1;
                    HeadDirection = Directions.Left;
                    break;

                case Directions.Right:
                    SnakeCells[0].x = SnakeCells[0].x + 1;
                    HeadDirection = Directions.Right;
                    break;
            }
            Head.x = SnakeCells[0].x;
            Head.y = SnakeCells[0].y;
        }
        public void MakeSnakeLonger()
        {
            SnakeCells.Add(new Cell
            {
                x = Tail.x,
                y = Tail.y
            });
            Length++;
        }
        public void ChangeSnakeHeadPicture(List<Cell> cells)
        {
            switch (HeadDirection)
            {
                case Directions.Up:
                    cells.Find(c => c.x == Head.x && c.y == Head.y).Color = GridColors.EyesUp;
                    break;

                case Directions.Down:
                    cells.Find(c => c.x == Head.x && c.y == Head.y).Color = GridColors.EyesDown;

                    break;

                case Directions.Left:
                    cells.Find(c => c.x == Head.x && c.y == Head.y).Color = GridColors.EyesLeft;
                    break;

                case Directions.Right:
                    cells.Find(c => c.x == Head.x && c.y == Head.y).Color = GridColors.EyesRight;
                    break;
            }
        }
        public void ChangeSnakeColor(string color, List<Cell> cells)
        {
            foreach (var cell in SnakeCells)
            {
                cells.Find(c => c.x == cell.x && c.y == cell.y).Color = color;
            }
            cells.Find(c => c.x == Tail.x && c.y == Tail.y).Color = color;
        }
    }
}
