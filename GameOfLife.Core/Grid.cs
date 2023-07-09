using System;

namespace GameOfLife.Core
{
    public class Grid
    {
        public int Width {get;set;}
        public int Height {get;set;}

        public Grid(int width, int height)
        {
            if(width <= 0 || width > 25 || height <= 0 || height > 25)
            throw new ArgumentException("Dimensions must be between 1 to 25 inclusive!");

            this.Width = width;
            this.Height = height;
        }
    }
}
