using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace GameOfLife.Core
{
    public class Grid
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Dictionary<(int, int), bool> Cells { get; set; } = new Dictionary<(int, int), bool>();


        public Grid(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
