using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core
{
    public class ReproductionStrategy : ICellUpdateStrategy
    {
        public bool UpdateCellState(bool currentState)
        {
            return true;
        }
    }
}
