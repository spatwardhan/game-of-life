using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core
{
    public class DefaultStrategy : ICellUpdateStrategy
    {
        public bool UpdateCellState(bool currentState)
        {
            return currentState;
        }
    }
}
