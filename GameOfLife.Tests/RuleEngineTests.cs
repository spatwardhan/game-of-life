using System;
using Xunit;
using GameOfLife.Core;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests
{
    public class RuleEngineTests
    {
        [Fact]
        public void ShouldNotChangeGridIfNoLiveCellsAvailable()
        {
            var grid = new Grid(2, 2);
            RuleEngine.MoveToNextGeneration(grid);

            Assert.NotNull(grid.Cells);
            Assert.DoesNotContain(grid.Cells, cell => cell.Value);
        }

        [Fact]
        public void ShouldHandleGridChanges()
        {
            var grid = new Grid(3, 3);
            var liveCells = new List<(int, int)> { (1, 0), (1, 1), (2, 0) };
            GridOperations.InitializeGrid(grid);
            GridOperations.UpdateGrid(grid, liveCells);
            RuleEngine.MoveToNextGeneration(grid);

            Assert.NotNull(grid.Cells);
            var actual = grid.Cells.Where(cell => cell.Value);
            Assert.Equal(4, actual.Count());
            Assert.Collection(actual,
                cell1 => cell1.Key.Equals((1, 0)),
                cell2 => cell2.Key.Equals((1, 1)),
                cell3 => cell3.Key.Equals((2, 0)),
                cell4 => cell4.Key.Equals((2, 1)));
        }

        [Fact]
        public void ShouldHandleGridChangesAcrossMultipleGenerations()
        {
            var grid = new Grid(5, 5);
            var liveCells = new List<(int,int)> { (1, 2), (1, 3), (1, 4), (2, 2), (2, 3), (2, 4), (3, 1), (3, 4) };
            GridOperations.InitializeGrid(grid);
            GridOperations.UpdateGrid(grid, liveCells);

            for (int i = 0; i < 3; i++)
            {
                RuleEngine.MoveToNextGeneration(grid);
            }

            Assert.NotNull(grid.Cells);
            var actual = grid.Cells.Where(cell => cell.Value);
            Assert.Equal(7, actual.Count());
            Assert.Collection(actual,
                cell1 => cell1.Key.Equals((1, 2)),
                cell2 => cell2.Key.Equals((1, 3)),
                cell3 => cell3.Key.Equals((2, 1)),
                cell4 => cell4.Key.Equals((2, 3)),
                cell5 => cell5.Key.Equals((3, 1)),
                cell6 => cell6.Key.Equals((3, 2)),
                cell7 => cell7.Key.Equals((3, 3)));
        }
    }
}
