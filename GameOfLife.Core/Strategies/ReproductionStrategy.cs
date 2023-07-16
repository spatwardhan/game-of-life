using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core.Strategies
{
    public class ReproductionStrategy : ICellUpdateStrategy
    {
        public bool UpdateCellState(bool currentState)
        {
            return true;
        }
    }
}
