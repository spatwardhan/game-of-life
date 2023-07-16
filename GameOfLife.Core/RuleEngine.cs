    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Schema;

namespace GameOfLife.Core
{
    public static class RuleEngine
    {
        private static readonly Dictionary<Scenario, ICellUpdateStrategy> UpdateStrategies = new Dictionary<Scenario, ICellUpdateStrategy>
        {
            { Scenario.UNDERPOPULATION, new UnderpopulationStrategy() },
            { Scenario.OVERPOPULATION, new OverpopulationStrategy()},
            { Scenario.REPRODUCTION, new ReproductionStrategy()},
            { Scenario.DEFAULT, new DefaultStrategy()}
        };

        public static void MoveToNextGeneration(Grid grid)
        {
            var cells = grid.Cells;
            var updatedCells = new Dictionary<Cell, bool>();
            var scenario = Scenario.DEFAULT;
            ICellUpdateStrategy strategy;

            foreach (var cell in cells.Keys)
            {
                var newCell = new Cell(cell);
                var liveNeighbourCount = GetLiveNeighbourCount(grid, cell);
                scenario = GetScenarioForTransition(cells[cell], liveNeighbourCount);

                strategy = UpdateStrategies[scenario];
                var newState = strategy.UpdateCellState(cells[cell]);

                updatedCells.Add(newCell, newState);
            }
            grid.Cells = updatedCells;
        }

        public static Scenario GetScenarioForTransition(bool isLive, int liveNeighbourCount)
        {
            if (isLive && (liveNeighbourCount < 2))
                return Scenario.UNDERPOPULATION;
            else if (isLive && (liveNeighbourCount > 3))
                return Scenario.OVERPOPULATION;
            else if ((!isLive) && (liveNeighbourCount == 3))
                return Scenario.REPRODUCTION;
            return Scenario.DEFAULT;
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