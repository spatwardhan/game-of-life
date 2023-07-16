using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core.Strategies
{
    internal class OverpopulationStrategy : ICellUpdateStrategy
    {
        public bool UpdateCellState(bool currentState)
        {
            return false;
        }
    }
}
