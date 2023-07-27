﻿namespace SnakeGameBlazor.Data
{
    public class Snake
    {
        public List<Cell> Cells { get; set; }
        public int Length { get; set; }
        public Cell Head { get; set; }
        public Cell Tail { get; set; }

        public Snake()
        {
            Cells = new List<Cell>();
            Head = new Cell();
            Tail = new Cell();
        }
    }
}
