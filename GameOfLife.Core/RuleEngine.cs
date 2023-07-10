using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Core
{
    public class RuleEngine
    {
        private Grid _grid;

        public RuleEngine(Grid grid)
        {
            _grid = grid;
        }

        public (Cell,bool) MoveToNextGeneration(Cell cell)
        {
            var cells = _grid.GetCells();
            var (x,y) = (cell.X,cell.Y);
            var liveNeighbourCount = GetNeighbours(x,y).Where(isLive => isLive).Count();
            if(cells[x,y])
            {
                if(liveNeighbourCount < 2)
                    return (cell,false);                
            }
            return (cell,true);
        }

        private IEnumerable<bool> GetNeighbours(int x,int y)
        {
            var (w,h) = (_grid.Width, _grid.Height);
            var cells = _grid.GetCells();
            var neighbours = new List<Cell>
            {
                new Cell(x-1,y-1),
                new Cell(x-1,y),
                new Cell(x-1,y+1),
                new Cell(x,y-1),
                new Cell(x,y+1),
                new Cell(x+1,y-1),
                new Cell(x+1,y),
                new Cell(x+1,y+1)
            };

            return neighbours
            .Where(cell => cell.X >= 0 && cell.X <= h-1 && cell.Y >= 0 && cell.Y <= w-1)
            .Select(cell=>cells[cell.X,cell.Y]);
        }
    }
}