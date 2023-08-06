using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core.Strategies
{
    public class LiveCellUpdateStrategy : ICellUpdateStrategy
    {
        public bool UpdateCellState(int liveNeighbourCount)
        {
            if (liveNeighbourCount == 2 || liveNeighbourCount == 3)
                return true;

            return false;
        }
    }
}
