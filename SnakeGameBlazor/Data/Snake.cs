using Microsoft.AspNetCore.Components;
using SnakeGameBlazor.Constants;
using SnakeGameBlazor.Contracts;
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

        public static int InitialLenght;
        public static int InitialSpeed;

        public Snake(int initialLength, int initialSpeed)
        {
            if (initialLength < 1 || initialSpeed < 1)
            {
                throw new ArgumentOutOfRangeException("Snake length and snake speed cannot be less than 1");
            }

            InitialLenght = initialLength;
            InitialSpeed = initialSpeed;
            SnakeCells = new List<Cell>();
            Head = new Cell();
            Tail = new Cell();
            Length = InitialLenght;
            Speed = InitialSpeed;
        }

        public void InitializeSnake(int gridSize, ICells cells)
        {
            if (SnakeCells.Count > 0)
            {
                SnakeCells.Clear();
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

            cells.Get(Head.x, Head.y).Color = GridColors.EyesDown;
        }

        public void MoveBody()
        {

            Tail.x = SnakeCells[Length - 1].x;
            Tail.y = SnakeCells[Length - 1].y;

            for (int i = Length - 1; i > 0; i--)
            {
                SnakeCells[i].x = SnakeCells[i - 1].x;
                SnakeCells[i].y = SnakeCells[i - 1].y;
            }
        }

        public void MoveHead(string direction)
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

        public void HideTail(ICells cells)
        {
            cells.Get(Tail.x, Tail.y).Color = GridColors.Green;
        }

        public void Shorten(int cellsCount)
        {
            if (cellsCount < Length)
            {
                for (int i = 0; i < cellsCount; i++)
                {
                    SnakeCells.RemoveAt(SnakeCells.Count-1);
                    Tail.x = SnakeCells[Length - 1].x;
                    Tail.y = SnakeCells[Length - 1].y;
                }
            }

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
        public void ChangeSnakeHeadPicture(ICells cells)
        {
            switch (HeadDirection)
            {
                case Directions.Up:
                    cells.Get(Head.x, Head.y).Color = GridColors.EyesUp;
                    break;

                case Directions.Down:
                    cells.Get(Head.x, Head.y).Color = GridColors.EyesDown;

                    break;

                case Directions.Left:
                    cells.Get(Head.x, Head.y).Color = GridColors.EyesLeft;
                    break;

                case Directions.Right:
                    cells.Get(Head.x, Head.y).Color = GridColors.EyesRight;
                    break;
            }
        }
        public void ChangeSnakeColor(string color, ICells cells)
        {
            foreach (var snakeCell in SnakeCells)
            {
                cells.Get(snakeCell.x, snakeCell.y).Color = color;
            }
        }
    }
}
