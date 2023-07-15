using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace GameOfLife.Core
{
    public class RuleEngine
    {
        private Grid _grid;
        private Dictionary<Scenario, ICellUpdateStrategy> _updateStrategies;

        public RuleEngine(Grid grid)
        {
            _grid = grid;
            _updateStrategies = new Dictionary<Scenario, ICellUpdateStrategy>
            {
                { Scenario.UNDERPOPULATION, new UnderpopulationStrategy() },
                { Scenario.OVERPOPULATION, new OverpopulationStrategy()},
                { Scenario.REPRODUCTION, new ReproductionStrategy()},
                { Scenario.DEFAULT, new DefaultStrategy()}
};
        }

        public void MoveToNextGeneration()
        {
            var cells = _grid.Cells;
            var updatedCells = new Dictionary<Cell, bool>();
            var scenario = Scenario.DEFAULT;
            ICellUpdateStrategy strategy;

            foreach (var cell in cells.Keys)
            {
                var newCell = new Cell(cell);
                var liveNeighbourCount = GetLiveNeighbourCount(cell);
                if (cells[cell] && (liveNeighbourCount < 2))
                    scenario = Scenario.UNDERPOPULATION;
                else if (cells[cell] && (liveNeighbourCount > 3))
                    scenario = Scenario.OVERPOPULATION;
                else if ((!cells[cell]) && (liveNeighbourCount == 3))
                    scenario = Scenario.REPRODUCTION;

                strategy = _updateStrategies[scenario];
                var newState = strategy.UpdateCellState(cells[cell]);

                updatedCells.Add(newCell, newState);
            }
            _grid.Cells = updatedCells;
        }

        private int GetLiveNeighbourCount(Cell cell)
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
                if (_grid.Cells.TryGetValue(neighbour, out var isLive) && isLive)
                    ++liveCount;
            }

            return liveCount;
        }
    }
}