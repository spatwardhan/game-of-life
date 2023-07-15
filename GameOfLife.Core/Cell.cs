using System;
using System.Collections.Generic;

namespace GameOfLife.Core
{
    public struct Cell
    {
        public int X {get;set;}
        public int Y {get;set;}

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Cell(Cell cell)
        {
            this.X = cell.X;
            this.Y = cell.Y;
        }
    }

    public class CellComparer : IEqualityComparer<Cell>
    {
        public bool Equals(Cell first, Cell second)
        {
            return (first.X == second.X) && (first.Y == second.Y);
        }
        public int GetHashCode(Cell cell)
        {
            return cell.X.GetHashCode() ^ cell.Y.GetHashCode();
        }
    }
}