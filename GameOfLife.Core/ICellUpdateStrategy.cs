using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core
{
    public interface ICellUpdateStrategy
    {
        bool UpdateCellState(bool currentState);
    }
}
