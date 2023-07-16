using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core
{
    public class Settings
    {
        public Grid Grid { get; set; }
        public int Generations { get; set; }
        public List<Cell> LiveCells { get; set; }

        public int CurrentGeneration { get; set; }

        public int MinGenerations => 3;

        public int MaxGenerations => 20;
    }
}
