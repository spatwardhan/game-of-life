using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Schema;
using GameOfLife.Core.Strategies;

namespace GameOfLife.Core
{
    public static class RuleEngine
    {
        public static void MoveToNextGeneration(Grid grid)
        {
            var cells = grid.Cells;
            var updatedCells = new Dictionary<Cell, bool>();
            ICellUpdateStrategy strategy;

            foreach (var cell in cells.Keys)
            {
                var newCell = new Cell(cell);
                var liveNeighbourCount = GetLiveNeighbourCount(grid, cell);

                strategy = CellUpdateStrategyFactory.GetStrategy(cells[cell]);
                var newState = strategy.UpdateCellState(liveNeighbourCount);

                updatedCells.Add(newCell, newState);
            }
            grid.Cells = updatedCells;
        }

        private static int GetLiveNeighbourCount(Grid grid, Cell cell)
        {
            var (x, y) = (cell.X, cell.Y);
            var neighbours = new List<Cell>
            {
                new Cell(x-1,y-1),
                new Cell(x-1,y),
                new Cell(x-1,y+1),
                new Cell(x,y-1),
                new Cell(x,y+1),
                new Cell(x+1,y-1),
                new Cell(x+1,y),
                new Cell(x+1,y+1)
            };

            int liveCount = 0;

            foreach (var neighbour in neighbours)
            {
                if (grid.Cells.TryGetValue(neighbour, out var isLive) && isLive)
                    ++liveCount;
            }

            return liveCount;
        }
    }
}