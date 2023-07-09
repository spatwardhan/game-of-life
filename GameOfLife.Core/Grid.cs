using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Core
{
    public class Grid
    {
        public int Width {get;set;}
        public int Height {get;set;}
        private List<Cell> LiveCells;

        public Grid(int width, int height)
        {
            if(width <= 0 || width > 25 || height <= 0 || height > 25)
            throw new ArgumentException("Dimensions must be between 1 to 25 inclusive!");

            this.Width = width;
            this.Height = height;
            this.LiveCells = new List<Cell>();
        }

        public void ResetGrid()
        {
            this.LiveCells.Clear();
        }

        public void UpdateGrid(List<Cell> liveCells)
        {
            this.LiveCells = liveCells;
        }

        public void ShowGrid()
        {
            for(int i=0;i<this.Width;i++)
            {
                for(int j=0;j<this.Height;j++)
                {
                    if(this.LiveCells.Any(cell => cell.X == i && cell.Y == j))
                        Console.Write("o ");
                    else
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
        }
    }
}
