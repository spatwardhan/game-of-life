using System;

namespace GameOfLife.Core
{
    public class Cell
    {
        public int X {get;set;}
        public int Y {get;set;}

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}