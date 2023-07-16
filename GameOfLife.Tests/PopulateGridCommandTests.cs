using Xunit;
using GameOfLife.Core;
using GameOfLife.Core.Commands;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests
{
    public class PopulateGridCommandTests
    {
        [Fact]
        public void ShouldSetLiveCellForValidInput()
        {
            var settings = new Settings { Grid = new Grid(5, 5), LiveCells = new List<Cell>() };
            var command = new PopulateGridCommand();
            var result = command.Execute(settings, "2 3");
            Assert.Equal(Status.CONTINUE, result.Status);
            Assert.Null(result.MessageText);
            Assert.Single(settings.LiveCells);
            Assert.Equal(new Cell(2, 3), settings.LiveCells[0]);
        }

        [Fact]
        public void ShouldRejectOutOfRangeInput()
        {
            var settings = new Settings { Grid = new Grid(2, 2), LiveCells = new List<Cell>() };
            var command = new PopulateGridCommand();
            var result = command.Execute(settings, "2 3");
            Assert.Equal(Status.OUT_OF_RANGE, result.Status);
            Assert.Equal("Position must be within the grid dimensions!", result.MessageText);
            Assert.Empty(settings.LiveCells);
        }

        [Fact]
        public void ShouldRejectInvalidInput()
        {
            var settings = new Settings { Grid = new Grid(2, 2), LiveCells = new List<Cell>() };
            var command = new PopulateGridCommand();
            var result = command.Execute(settings, "not a number");
            Assert.Equal(Status.INVALID, result.Status);
            Assert.Equal("Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:", result.MessageText);
            Assert.Empty(settings.LiveCells);
        }

        [Fact]
        public void ShouldHandleReset()
        {
            var settings = new Settings { Grid = new Grid(2, 2) };
            var liveCell = new Cell(1, 0);
            settings.Grid.Cells[liveCell] = true;
            var command = new PopulateGridCommand();
            var result = command.Execute(settings, "~");
            Assert.Equal(Status.CONTINUE, result.Status);
            Assert.Equal("Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:", result.MessageText);
            var liveCells = settings.Grid.Cells.Where(cell => cell.Value);
            Assert.Empty(liveCells);
        }

        [Fact]
        public void ShouldHandleReturnToMainMenu()
        {
            var settings = new Settings { Grid = new Grid(2, 2) };
            var command = new PopulateGridCommand();
            var result = command.Execute(settings, "#");
            Assert.Equal(Status.VALID, result.Status);
            Assert.Null(result.MessageText);
        }
    }
}
