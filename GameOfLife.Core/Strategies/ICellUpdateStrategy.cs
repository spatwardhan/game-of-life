using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core.Strategies
{
    public interface ICellUpdateStrategy
    {
        bool UpdateCellState(int liveNeighbourCount);
    }
}
