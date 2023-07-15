using System;
using Xunit;
using GameOfLife.Core;
using System.Collections.Generic;

namespace GameOfLife.Tests
{
    public class RuleEngineTests
    {
        [Fact]
        public void LiveCellWithNoLiveNeighboursShouldDie()
        {     
            var grid = new Grid(2,2);
            var liveCells = new List<Cell>{new Cell(0,0)};
            grid.UpdateGrid(liveCells);
            var cell = new Cell(0,0);
            var engine = new RuleEngine(grid);
            engine.MoveToNextGeneration();

            var updatedCells = grid.Cells;
            Assert.NotNull(updatedCells);
            Assert.False(updatedCells[cell]);
        }

        [Fact]
        public void LiveCellWithExactlyOneLiveNeighbourShouldDie()
        {
            var grid = new Grid(2, 2);
            var liveCells = new List<Cell> { new Cell(0, 0), new Cell(1, 0) };
            grid.UpdateGrid(liveCells);
            var cell = new Cell(0, 0);
            var engine = new RuleEngine(grid);

            engine.MoveToNextGeneration();

            var updatedCells = grid.Cells;
            Assert.NotNull(updatedCells);
            Assert.False(updatedCells[cell]);
        }

        [Fact]
        public void LiveCellWithTwoLiveNeighboursShouldLive()
        {
            var grid = new Grid(2, 2);
            var liveCells = new List<Cell> { new Cell(0, 0), new Cell(0, 1), new Cell(1, 0) };
            grid.UpdateGrid(liveCells);
            var cell = new Cell(0, 0);
            var engine = new RuleEngine(grid);
            engine.MoveToNextGeneration();

            var updatedCells = grid.Cells;
            Assert.NotNull(updatedCells);
            Assert.True(updatedCells[cell]);
        }

        [Fact]
        public void LiveCellWithThreeLiveNeighboursShouldLive()
        {
            var grid = new Grid(2, 2);
            var liveCells = new List<Cell> { new Cell(0, 0), new Cell(0, 1), new Cell(1, 0), new Cell(1, 1) };
            grid.UpdateGrid(liveCells);
            var cell = new Cell(0, 0);
            var engine = new RuleEngine(grid);

            engine.MoveToNextGeneration();

            var updatedCells = grid.Cells;
            Assert.NotNull(updatedCells);
            Assert.True(updatedCells[cell]);
        }

        [Fact]
        public void LiveCellWithMoreThanThreeLiveNeighboursShouldDie()
        {
            var grid = new Grid(2, 3);
            var liveCells = new List<Cell> { new Cell(0, 0), new Cell(0, 1), new Cell(1, 0), new Cell(1, 1), new Cell(2, 0) };
            grid.UpdateGrid(liveCells);
            var cell = new Cell(1, 1);
            var engine = new RuleEngine(grid);

            engine.MoveToNextGeneration();

            var updatedCells = grid.Cells;
            Assert.NotNull(updatedCells);
            Assert.False(updatedCells[cell]);
        }

        [Fact]
        public void DeadCellWithExactlyThreeLiveNeighboursShouldBecomeAlive()
        {
            var grid = new Grid(2, 3);
            var liveCells = new List<Cell> { new Cell(0, 0), new Cell(0, 1), new Cell(1, 0) };
            grid.UpdateGrid(liveCells);
            var cell = new Cell(1, 1);
            var engine = new RuleEngine(grid);

            engine.MoveToNextGeneration();

            var updatedCells = grid.Cells;
            Assert.NotNull(updatedCells);
            Assert.True(updatedCells[cell]);
        }

        [Fact]
        public void DeadCellWithLessThanThreeLiveNeighboursShouldRemainDead()
        {
            var grid = new Grid(2, 3);
            var liveCells = new List<Cell> { new Cell(0, 0), new Cell(0, 1) };
            grid.UpdateGrid(liveCells);
            var cell = new Cell(1, 1);
            var engine = new RuleEngine(grid);
            engine.MoveToNextGeneration();

            var updatedCells = grid.Cells;
            Assert.NotNull(updatedCells);
            Assert.False(updatedCells[cell]);
        }
    }
}
