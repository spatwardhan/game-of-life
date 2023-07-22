using Xunit;
using GameOfLife.Core;
using GameOfLife.Core.Commands;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests
{
    public class PopulateGridCommandTests
    {
        private readonly Settings _settings = new Settings();
        private readonly PopulateGridCommand _command = new PopulateGridCommand();

        [Fact]
        public void ShouldSetLiveCellForValidInput()
        {
            _settings.Grid = new Grid(5, 5);
            _settings.LiveCells = new List<Cell>();

            var result = _command.Execute(_settings, "2 3");

            Assert.Equal(Status.CONTINUE, result.Status);
            Assert.Null(result.MessageText);
            Assert.Single(_settings.LiveCells);
            Assert.Equal(new Cell(2, 3), _settings.LiveCells[0]);
        }

        [Fact]
        public void ShouldRejectOutOfRangeInput()
        {
            _settings.Grid = new Grid(2, 2);
            _settings.LiveCells = new List<Cell>();

            var result = _command.Execute(_settings, "2 3");

            Assert.Equal(Status.OUT_OF_RANGE, result.Status);
            Assert.Equal("Position must be within the grid dimensions!", result.MessageText);
            Assert.Empty(_settings.LiveCells);
        }

        [Fact]
        public void ShouldRejectInvalidInput()
        {
            _settings.Grid = new Grid(2, 2);
            _settings.LiveCells = new List<Cell>();

            var result = _command.Execute(_settings, "not a number");

            Assert.Equal(Status.INVALID, result.Status);
            Assert.Equal("Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:", result.MessageText);
            Assert.Empty(_settings.LiveCells);
        }

        [Fact]
        public void ShouldHandleReset()
        {
            _settings.Grid = new Grid(2, 2);
            var liveCell = new Cell(1, 0);
            _settings.Grid.Cells[liveCell] = true;

            var result = _command.Execute(_settings, "~");

            Assert.Equal(Status.CONTINUE, result.Status);
            Assert.Equal("Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:", result.MessageText);
            var liveCells = _settings.Grid.Cells.Where(cell => cell.Value);
            Assert.Empty(liveCells);
        }

        [Fact]
        public void ShouldHandleReturnToMainMenu()
        {
            _settings.Grid = new Grid(2, 2);

            var result = _command.Execute(_settings, "#");
            
            Assert.Equal(Status.VALID, result.Status);
            Assert.Null(result.MessageText);
        }
    }
}
