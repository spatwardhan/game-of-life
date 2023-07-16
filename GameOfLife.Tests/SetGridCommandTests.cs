using Xunit;
using GameOfLife.Core;
using GameOfLife.Core.Commands;

namespace GameOfLife.Tests
{
    public class SetGridCommandTests
    {
        [Fact]
        public void ShouldSetGridForValidInput()
        {
            var settings = new Settings { Grid = new Grid(25, 25) };
            var command = new SetGridCommand();
            var result = command.Execute(settings, "5 5");
            Assert.Equal(Status.VALID, result.Status);
            Assert.Null(result.MessageText);
            Assert.Equal(5, settings.Grid.Width);
            Assert.Equal(5, settings.Grid.Height);
        }

        [Fact]
        public void ShouldRejectOutOfRangeInput()
        {
            var settings = new Settings { Grid = new Grid(25, 25) };
            var command = new SetGridCommand();
            var result = command.Execute(settings, "50 50");
            Assert.Equal(Status.OUT_OF_RANGE, result.Status);
            Assert.Equal("Dimensions must be between 1 to 25 inclusive!", result.MessageText);
            Assert.Equal(25, settings.Grid.Width);
            Assert.Equal(25, settings.Grid.Height);
        }
    }
}
