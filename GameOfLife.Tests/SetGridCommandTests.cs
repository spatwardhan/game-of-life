using Xunit;
using GameOfLife.Core;
using GameOfLife.Core.Commands;

namespace GameOfLife.Tests
{
    public class SetGridCommandTests
    {
        private readonly Settings _settings = new Settings { Grid = new Grid(25, 25) };
        private readonly SetGridCommand _command = new SetGridCommand();

        [Fact]
        public void ShouldSetGridForValidInput()
        {
            var result = _command.Execute(_settings, "5 5");

            Assert.Equal(Status.VALID, result.Status);
            Assert.Null(result.MessageText);
            Assert.Equal(5, _settings.Grid.Width);
            Assert.Equal(5, _settings.Grid.Height);
        }

        [Fact]
        public void ShouldRejectOutOfRangeInput()
        {
            var result = _command.Execute(_settings, "50 50");

            Assert.Equal(Status.OUT_OF_RANGE, result.Status);
            Assert.Equal("Dimensions must be between 1 to 25 inclusive!", result.MessageText);
            Assert.Equal(25, _settings.Grid.Width);
            Assert.Equal(25, _settings.Grid.Height);
        }
    }
}
