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
            var updatedCells = new Dictionary<(int,int), bool>();
            ICellUpdateStrategy strategy;

            foreach (var cell in cells.Keys)
            {
                var newCell = cell;
                var liveNeighbourCount = GetLiveNeighbourCount(grid, cell);

                strategy = CellUpdateStrategyFactory.GetStrategy(cells[cell]);
                var newState = strategy.UpdateCellState(liveNeighbourCount);

                updatedCells.Add(newCell, newState);
            }
            grid.Cells = updatedCells;
        }

        private static int GetLiveNeighbourCount(Grid grid, (int,int) cell)
        {
            var (x, y) = cell;
            var neighbours = new List<(int,int)>
            {
                (x-1,y-1),
                (x-1,y),
                (x - 1, y + 1),
                (x, y - 1),
                (x, y + 1),
                (x + 1, y - 1),
                (x + 1, y),
                (x + 1, y + 1)
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