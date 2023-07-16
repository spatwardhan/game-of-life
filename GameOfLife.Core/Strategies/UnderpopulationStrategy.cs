using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core.Strategies
{
    public class UnderpopulationStrategy : ICellUpdateStrategy
    {
        public bool UpdateCellState(bool currentState)
        {
            return false;
        }
    }
}
