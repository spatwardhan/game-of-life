using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Core
{
    public class GridOperations
    {
        private Grid _grid;

        public GridOperations(Grid grid)
        {
            _grid = grid;
        }

        public void ResetGrid()
        {
            if (_grid.Cells.Any())
                _grid.Cells.Clear();
            InitializeGrid();
        }

        public void InitializeGrid()
        {
            for (int i = 0; i < _grid.Height; i++)
            {
                for (int j = 0; j < _grid.Width; j++)
                {
                    var cell = new Cell(i, j);
                    _grid.Cells.Add(cell, false);
                }
            }
        }

        public void UpdateGrid(List<Cell> liveCells)
        {
            foreach (var liveCell in liveCells)
            {
                _grid.Cells[liveCell] = true;
            }
        }

        public void ShowGrid()
        {
            for (int i = 0; i < _grid.Height; i++)
            {
                for (int j = 0; j < _grid.Width; j++)
                {
                    var cell = new Cell(i, j);
                    Console.Write(_grid.Cells[cell] ? "o " : ". ");
                }
                Console.WriteLine();
            }
        }
    }
}
