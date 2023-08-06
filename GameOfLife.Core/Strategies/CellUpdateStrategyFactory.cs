using GameOfLife.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core.Strategies
{
    public static class CellUpdateStrategyFactory
    {
        public static ICellUpdateStrategy GetStrategy(bool isLive)
        {
            if (isLive)
                return new LiveCellUpdateStrategy();
            else
                return new DeadCellUpdateStrategy();
        }
    }
}
