using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Core
{
    public class Grid
    {
        public int Width {get;set;}
        public int Height {get;set;}
        private bool[,] _cells;

        public Grid(int width, int height)
        {
            if(width <= 0 || width > 25 || height <= 0 || height > 25)
            throw new ArgumentException("Dimensions must be between 1 to 25 inclusive!");

            this.Width = width;
            this.Height = height;
            _cells = new bool[height,width];
        }

        public void ResetGrid()
        {
            for (int i=0;i<this.Height;i++) 
            {
                for(int j=0;j<this.Width;j++)
                {
                    _cells[i,j] = false;
                }
            }
        }

        public void UpdateGrid(List<Cell> liveCells)
        {
            foreach(var cell in liveCells)
            {
                Console.WriteLine($"{cell.X},{cell.Y}");
                _cells[cell.X,cell.Y] = true;
            }
        }

        public void ShowGrid()
        {
            for(int i=0;i<this.Height;i++)
            {
                for(int j=0;j<this.Width;j++)
                {
                    Console.Write(_cells[i,j] ? "o " : ". ");
                }
                Console.WriteLine();
            }
        }

        public bool[,] GetCells()
        {
            return _cells;
        }
    }
}
