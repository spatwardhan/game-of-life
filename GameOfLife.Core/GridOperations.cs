using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Core
{
    public static class GridOperations
    {
        public static void ResetGrid(Grid grid)
        {
            if (grid.Cells.Any())
                grid.Cells.Clear();
            InitializeGrid(grid);
        }

        public static void InitializeGrid(Grid grid)
        {
            for (int i = 0; i < grid.Height; i++)
            {
                for (int j = 0; j < grid.Width; j++)
                {
                    var cell = new Cell(i, j);
                    grid.Cells.Add(cell, false);
                }
            }
        }

        public static void UpdateGrid(Grid grid, List<Cell> liveCells)
        {
            foreach (var liveCell in liveCells)
            {
                grid.Cells[liveCell] = true;
            }
        }

        public static void ShowGrid(Grid grid)
        {
            for (int i = 0; i < grid.Height; i++)
            {
                for (int j = 0; j < grid.Width; j++)
                {
                    var cell = new Cell(i, j);
                    Console.Write(grid.Cells[cell] ? "o " : ". ");
                }
                Console.WriteLine();
            }
        }
    }
}
