using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core
{
    public class UnderpopulationStrategy : ICellUpdateStrategy
    {
        public bool UpdateCellState(bool currentState)
        {
            return false;
        }
    }
}
